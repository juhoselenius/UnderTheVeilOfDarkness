using Logic.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        private IPlayerManager _playerManager;

        private float speed;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;
        public float jumpSpeed;
        public float gravity;
        public float jumpHeight;
        
        public float knockBackForce;
        public KeyCode sprintKey = KeyCode.LeftShift;

        public Transform groundCheck;
        public float groundDistance;
        public LayerMask groundMask;
        public MovementState state;
        private int maxJumpCount = 2;
        public int jumpRemaining;
        public float dodgeSpeed;
        public float dodgeTime;
        public float dodgetimer = 1.5f;
        public float dodgeTimefull;
        public float dodgeSpeedfull;
        public float nextdodge;

        public Sprite[] movementSprite;
        public Image movementIcon;
        public Image filler;

        Vector3 velocity;
        bool isGrounded;
    
        public enum MovementState
        {
            walking,
            //sprinting,
            air
        }

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();          
        }

        void Start()
        {
            //walkSpeed = 2f + 0.05f * _playerManager.GetMovement();
            //sprintSpeed = 2f + 0.1f * _playerManager.GetMovement();
            ChangeSpeed(_playerManager.GetMovement());
        }
    
        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y <0)
            {
                velocity.y = -2f;
                jumpRemaining = maxJumpCount;
            }

            StateHandler();
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && (jumpRemaining > 0) && _playerManager.GetMovement() >= 2)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jumpRemaining -= 1;
            }

            if (Input.GetButtonDown("Jump") && isGrounded && _playerManager.GetMovement() == 1)
            {
               velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (Input.GetKeyDown(sprintKey) && _playerManager.GetMovement() == 3 && Time.time > nextdodge)
            {
                StartCoroutine(Dodge());
                nextdodge = Time.time + dodgetimer;
            }

            if (Input.GetKeyDown(sprintKey) && _playerManager.GetMovement() == 4 && Time.time > nextdodge)
            {
                StartCoroutine(Dodgefull());
                nextdodge = Time.time + dodgetimer;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if(nextdodge > Time.time)
            {
                filler.fillAmount = (nextdodge - Time.time) / dodgetimer;
            }
            else
            {
                filler.fillAmount = 0;
            }
        }

        private void OnEnable()
        {
            _playerManager.MovementChanged += ChangeSpeed;
            _playerManager.MovementChanged += UpdateDash;
            ChangeSpeed(_playerManager.GetMovement());
            UpdateDash(_playerManager.GetMovement());
        }

        private void OnDisable()
        {
            _playerManager.MovementChanged -= ChangeSpeed;
            _playerManager.MovementChanged -= UpdateDash;
        }

        void ChangeSpeed(float newValue)
        {
            switch(newValue)
            {
                case 0:
                    walkSpeed = 1.5f;
                    break;
                case 1:
                    walkSpeed = 2f;
                    break;
                case 2:
                    walkSpeed = 3f;
                    break;
                case 3:
                    walkSpeed = 4f;
                    break;
                case 4:
                    walkSpeed = 5f;
                    break;
                
            }
            //walkSpeed = 1.5f + 0.375f * newValue;
            //sprintSpeed = 1f + newValue;
        }

        private void StateHandler()
        {
            //if (isGrounded && Input.GetKey(sprintKey) && _playerManager.GetMovement() >= 3)
            //{
              //  state = MovementState.sprinting;
                //speed = sprintSpeed;
            //}
            if (isGrounded)
            {
                state = MovementState.walking;
                speed = walkSpeed;
            }
            else
            {
                state = MovementState.air;
            }
        }

        IEnumerator Dodge()
        {
            float startTime = Time.time;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
            while (Time.time < startTime + dodgeTime)
            {
                controller.Move(move * dodgeSpeed * Time.deltaTime);
                yield return null;
            }
        }

        IEnumerator Dodgefull()
        {
            float startTime = Time.time;
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
            while (Time.time < startTime + dodgeTimefull)
            {
                controller.Move(move * dodgeSpeedfull * Time.deltaTime);
                yield return null;
            }
        }

        void UpdateDash(float newValue)
        {
            switch (newValue)
            {
                case 0:
                    movementIcon.sprite = movementSprite[0];
                    break;
                case 1:
                    movementIcon.sprite = movementSprite[0];
                    break;
                case 2:
                    movementIcon.sprite = movementSprite[0];
                    break;
                case 3:
                    movementIcon.sprite = movementSprite[1];
                    break;
                case 4:
                    movementIcon.sprite = movementSprite[1];
                    break;
            }
        }
    }
}

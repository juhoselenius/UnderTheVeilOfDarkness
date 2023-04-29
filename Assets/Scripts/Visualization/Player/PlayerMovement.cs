using Logic.Player;
using System.Collections;
using UnityEngine;

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
 

        Vector3 velocity;
        bool isGrounded;
    
        public enum MovementState
        {
            walking,
            sprinting,
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

        
            
            if (Input.GetKeyDown(KeyCode.LeftControl) && _playerManager.GetMovement() == 4)
            {
              
                    StartCoroutine(Dodge());
                   
                
            }
           

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        private void OnEnable()
        {
            _playerManager.MovementChanged += ChangeSpeed;
            ChangeSpeed(_playerManager.GetMovement());
        }

        private void OnDisable()
        {
            _playerManager.MovementChanged -= ChangeSpeed;
        }

        void ChangeSpeed(float newValue)
        {
            walkSpeed = 1.5f + 0.375f * newValue;
            sprintSpeed = 1f + newValue;
        }

        private void StateHandler()
        {
            if (isGrounded && Input.GetKey(sprintKey) && _playerManager.GetMovement() >= 3)
            {
                state = MovementState.sprinting;
                speed = sprintSpeed;
            }
            else if (isGrounded)
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
    }
}

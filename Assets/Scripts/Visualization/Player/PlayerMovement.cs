using Logic.Player;
using System.Collections;
using System.Collections.Generic;
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

        public KeyCode sprintKey = KeyCode.LeftShift;

        public Transform groundCheck;
        public float groundDistance;
        public LayerMask groundMask;
        public MovementState state;

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
            walkSpeed = 2f + 0.05f * _playerManager.GetMovement();
            sprintSpeed = 2f + 0.1f * _playerManager.GetMovement();
        }
    
        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y <0)
            {
                velocity.y = -2f;
            }
            StateHandler();
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);

            if(Input.GetButtonDown("Jump")&& isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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
            walkSpeed = 1f + 0.1f * newValue;
            sprintSpeed = 1f + 0.2f * newValue;
        }

        private void StateHandler()
        {
            if (isGrounded && Input.GetKey(sprintKey))
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
    }
}

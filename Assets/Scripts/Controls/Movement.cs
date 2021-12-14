using System;
using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace Shooter.Controls
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
    public class Movement : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 4.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Rotation speed of the character")]
        public float RotationSpeed = 1.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        [Tooltip("Rotation speed of character in m/s")]
        public float rollSpeed = 5.0f;
        [Tooltip("Vertical hover speed of the character")]
        public float hoverSpeed = 3.0f;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;
        
        [SerializeField] Light headlightsComponent;

        // cinemachine
        float _cinemachineTargetPitch;

        // player
        float _speed;
        float _rotationVelocityX;
        float _rotationVelocityY;
        float _rotationVelocityClockwise;
        float _verticalVelocity;
        float _terminalVelocity = 53.0f;

        CharacterController _controller;
        PlayerInput _input;
        GameObject _mainCamera;

        const float _threshold = 0.01f;
        bool enableControls = true;
        public bool EnableControls { set { enableControls = value; } }

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {

            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInput>();

            StartCoroutine("ResetRotation");
        }

        //reset rotation to avoid jagged camera movement due to early mouse movement
        IEnumerator ResetRotation()
        {
            yield return new WaitForEndOfFrame();
            //reset rotation
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        private void Update()
        {
            if (enableControls == false) { return; }

            RotatePlayer();
            RollPlayer();
            Move();
            TurnHeadlightsOnOff();
        }

        private void TurnHeadlightsOnOff()
        {
            headlightsComponent.enabled = _input.headlights;
        }

        private void RotatePlayer()
        {
            // if there is an input
            if (_input.look.sqrMagnitude >= _threshold)
            {
                _rotationVelocityX = _input.look.x * RotationSpeed * Time.deltaTime;
                _rotationVelocityY = _input.look.y * RotationSpeed * Time.deltaTime;

                transform.Rotate(Vector3.up * _rotationVelocityX);
                transform.Rotate(Vector3.right * _rotationVelocityY);
            }
        }

        void RollPlayer()
        {
            _rotationVelocityClockwise = _input.roll * rollSpeed * Time.deltaTime;
            transform.Rotate(0, 0, _rotationVelocityClockwise);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            _verticalVelocity = _input.hoverVector.y;

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                // move
                inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
            }

            // move the player
            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * hoverSpeed * Time.deltaTime);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
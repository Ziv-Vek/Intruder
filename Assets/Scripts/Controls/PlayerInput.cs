using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace Shooter.Controls
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        Vector2 rollVector;
        public float roll = 0f;
        public bool jump;
        public bool sprint;
        public bool fire;
        public Vector2 hoverVector;

        [Header("Movement Settings")]
        public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnRoll(InputValue value)
        {
            RollInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnHover(InputValue value)
        {
            HoverInput(value.Get<Vector2>());
        }

        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }


#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void RollInput(Vector2 newRollDirection)
        {
            rollVector = newRollDirection;

            roll = rollVector.x;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void HoverInput(Vector2 newHoverDirection)
        {
            hoverVector = newHoverDirection;
        }

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }

    }

}
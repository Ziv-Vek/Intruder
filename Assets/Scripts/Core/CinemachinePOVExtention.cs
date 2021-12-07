using UnityEngine;
using Cinemachine;
using Shooter.Controls;

public class CinemachinePOVExtention : CinemachineExtension
{
    Movement movement;

    // private PlayerInput playerInput;
    private Vector3 startingRotation;

    // float rotationSpeed;

    protected override void Awake() {
        // playerInput = FindObjectOfType<PlayerInput>();
        // rotationSpeed = FindObjectOfType<Movement>().RotationSpeed;
        movement = FindObjectOfType<Movement>();
        base.Awake();
    }
    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
     CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = movement.transform.localRotation.eulerAngles;
                state.RawOrientation = Quaternion.Euler(movement.transform.rotation.y, movement.transform.rotation.x, movement.transform.rotation.z);
                // Vector2 deltaInput = playerInput.look;
                // startingRotation.x += deltaInput.x * rotationSpeed * Time.deltaTime;
                // startingRotation.y += deltaInput.y * rotationSpeed * Time.deltaTime;
                // state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}

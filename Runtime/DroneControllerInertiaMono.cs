using UnityEngine;
using UnityEngine.InputSystem; // Required for InputActionReference

public class DroneControllerInertiaMono : MonoBehaviour
{
    public InputActionReference moveAction;    // For horizontal and vertical movement
    public InputActionReference yawAction;     // For rotation (yaw)
    public InputActionReference downUpAction;  // For ascending/descending

    public Rigidbody droneRigidbody;  // Rigidbody component for the drone
    public float forceAmount = 10f;   // Amount of force applied for movement
    public float liftForceAmount = 10f; // Force for vertical movement (up/down)
    public float rotationAmount = 5f; // Torque applied for yaw rotation

    void FixedUpdate()
    {
        // Get the input values from InputActionReference
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>(); // Left stick for horizontal and vertical movement
        float yawInput = yawAction.action.ReadValue<float>();       // Right stick for yaw rotation
        float upDownInput = downUpAction.action.ReadValue<float>(); // Triggers or right stick Y-axis for up/down movement

        // Apply forces for movement (strafe left/right and forward/backward)
        Vector3 forceDirection = new Vector3(moveInput.x, 0f, moveInput.y); // X for left/right, Z for forward/backward
        droneRigidbody.AddRelativeForce(forceDirection * forceAmount * Time.fixedDeltaTime);

        // Apply force for vertical movement (up/down)
        Vector3 verticalForce = new Vector3(0f, upDownInput * liftForceAmount, 0f);
        droneRigidbody.AddForce(verticalForce*Time.fixedDeltaTime);

        // Apply torque for yaw rotation (rotate left/right around the Y-axis)
        droneRigidbody.AddTorque(Vector3.up * yawInput * rotationAmount * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        // Enable input actions when this component is enabled
        moveAction.action.Enable();
        yawAction.action.Enable();
        downUpAction.action.Enable();
    }   

    private void OnDisable()
    {
        // Disable input actions when this component is disabled
        moveAction.action.Disable();
        yawAction.action.Disable();
        downUpAction.action.Disable();
    }
}

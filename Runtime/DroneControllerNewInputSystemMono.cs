using UnityEngine;
using UnityEngine.InputSystem; // Required for InputActionReference

public class DroneControllerNewInputSystem : MonoBehaviour
{
    public InputActionReference moveAction; // For horizontal and vertical movement
    public InputActionReference yawAction;  // For rotation (yaw)
    public InputActionReference upDownAction; // For ascending/descending

    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float liftSpeed = 5.0f;

    private Vector2 moveInput; // Store horizontal and vertical inputs
    private float yawInput;    // Store yaw input
    private float upDownInput; // Store up/down input

    void OnEnable()
    {
        // Subscribe to input actions
        moveAction.action.Enable();
        yawAction.action.Enable();
        upDownAction.action.Enable();

        // Bind input actions to methods
        moveAction.action.performed += OnMove;
        yawAction.action.performed += OnYaw;
        upDownAction.action.performed += OnUpDown;

        // Ensure actions are disabled when the object is disabled
        moveAction.action.canceled += ctx => moveInput = Vector2.zero;
        yawAction.action.canceled += ctx => yawInput = 0f;
        upDownAction.action.canceled += ctx => upDownInput = 0f;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        moveAction.action.Disable();
        yawAction.action.Disable();
        upDownAction.action.Disable();
    }

    // Method to handle move input (horizontal and vertical)
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Read X (horizontal) and Y (vertical)
    }

    // Method to handle yaw (rotation) input
    private void OnYaw(InputAction.CallbackContext context)
    {
        yawInput = context.ReadValue<float>(); // Read yaw (rotation)
    }

    // Method to handle up/down input
    private void OnUpDown(InputAction.CallbackContext context)
    {
        upDownInput = context.ReadValue<float>(); // Read up/down input
    }

    void Update()
    {
        // Apply movement based on input
        Vector3 moveDirection = new Vector3(moveInput.x, upDownInput, moveInput.y);
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);

        // Apply yaw (rotation)
        transform.Rotate(Vector3.up * yawInput * rotationSpeed * Time.deltaTime);
    }
}

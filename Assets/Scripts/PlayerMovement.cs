using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 4;
    [SerializeField] private float crouchSpeed = 2;
    [SerializeField] private float sprintSpeed = 6;
    [SerializeField] private float accelerationTime = 1;
    [SerializeField] private float counterAccelerationTime = 0.5f;
    [SerializeField] private float stopTime = 1;

    [SerializeField] private float gravity = -20;
    [SerializeField] private float jumpHeight = 2;

    [SerializeField] private float tiltStrength = 1;

    [SerializeField] private float crouchHeight = 0.5f;

    private CharacterController characterController;

    private Vector3 movementInput;
    private Vector3 velocity;

    private bool isCrouching;
    private bool isSprinting;

    private float Speed => isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : speed;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        UpdateInput();

        velocity.x = movementInput.x * Speed;
        velocity.z = movementInput.z * Speed;

        velocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded)
            if (Input.GetButtonDown("Jump"))
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            else
                velocity.y = -1;

        characterController.Move(transform.TransformVector(velocity * Time.deltaTime));

        UpdateTilt();

        transform.localScale = new Vector3(1, isCrouching ? crouchHeight : 1, 1);
    }

    private void UpdateInput() {
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(x) > 0)
            if (movementInput.x > 0 && x < 0 || movementInput.x < 0 && x > 0)
                movementInput.x += x * Time.deltaTime / counterAccelerationTime;
            else
                movementInput.x += x * Time.deltaTime / accelerationTime;
        else if (movementInput.x > 0) {
            movementInput.x -= Time.deltaTime / stopTime;
            if (movementInput.x < 0)
                movementInput.x = 0;
        } else if (movementInput.x < 0) {
            movementInput.x += Time.deltaTime / stopTime;
            if (movementInput.x > 0)
                movementInput.x = 0;
        }

        if (Mathf.Abs(z) > 0)
            if (movementInput.z > 0 && z < 0 || movementInput.z < 0 && z > 0)
                movementInput.z += z * Time.deltaTime / counterAccelerationTime;
            else
                movementInput.z += z * Time.deltaTime / accelerationTime;
        else if (movementInput.z > 0) {
            movementInput.z -= Time.deltaTime / stopTime;
            if (movementInput.z < 0)
                movementInput.z = 0;
        } else if (movementInput.z < 0) {
            movementInput.z += Time.deltaTime / stopTime;
            if (movementInput.z > 0)
                movementInput.z = 0;
        }

        movementInput = Vector3.ClampMagnitude(movementInput, 1);

        isCrouching = Input.GetKey(KeyCode.LeftControl);
        isSprinting = !isCrouching && z > 0 && (Input.GetKey(KeyCode.LeftShift) || isSprinting);
    }

    private void UpdateTilt() {
        transform.localEulerAngles = new Vector3(
            movementInput.z * tiltStrength,
            transform.localEulerAngles.y,
            -movementInput.x * tiltStrength
        );
    }
}
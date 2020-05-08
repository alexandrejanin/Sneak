using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField] private float sensitivity = 400f;

    private new Camera camera;

    private float pitch;

    private void Awake() {
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        var x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        pitch -= y;
        pitch = Mathf.Clamp(pitch, -89.9f, 89.9f);

        transform.Rotate(Vector3.up * x);
        camera.transform.localEulerAngles = pitch * Vector3.right;
    }
}
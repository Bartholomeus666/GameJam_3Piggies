using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    private InputAction _lookAction;
    private float _pitch;

    void Awake()
    {
        _lookAction = GetComponent<PlayerInput>().actions["Look"];
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 look = _lookAction.ReadValue<Vector2>() * mouseSensitivity;

        // Yaw rotates the body, pitch rotates only the camera
        transform.Rotate(Vector3.up * look.x);

        _pitch = Mathf.Clamp(_pitch - look.y, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _cameraSensitivity = 15f;
    [SerializeField] private float _minPitch = -80f;
    [SerializeField] private float _maxPitch = 80f;

    public bool DisableLook = false;

    private Vector2 _turnInput;
    private float _pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _pitch = transform.localEulerAngles.x;
    }

    public void Turn(InputAction.CallbackContext context)
    {
        _turnInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (DisableLook) return;

        float yaw = _turnInput.x * _cameraSensitivity * Time.deltaTime;
        _pitch -= _turnInput.y * _cameraSensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        _playerTransform.Rotate(Vector3.up * yaw);
        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

        // Reset so we don't keep rotating on stale input
        _turnInput = Vector2.zero;
    }
}
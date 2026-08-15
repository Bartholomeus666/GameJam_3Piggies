using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Vector3 _moveVector;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }


    public void Move(InputAction.CallbackContext context)
    {
        _moveVector.x = context.ReadValue<Vector2>().x;
        _moveVector.z = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded)
        {
            _moveVector.y = _jumpForce;
        }
    }

    private void AddGravity()
    {
        _moveVector.y += Physics.gravity.y * Time.fixedDeltaTime;
    }

    public void FixedUpdate()
    {
        AddGravity();

        _characterController.Move((transform.right * _moveVector.x * _speed + transform.forward * _moveVector.z * _speed + transform.up * _moveVector.y) * Time.fixedDeltaTime);
    }
}

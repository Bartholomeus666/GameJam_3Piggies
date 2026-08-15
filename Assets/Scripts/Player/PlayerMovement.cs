using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private Animator animator;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private InputAction _moveAction;

    void Awake()
    {
        _moveAction = GetComponent<PlayerInput>().actions["Move"];
    }

    void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 movement = transform.right * input.x + transform.forward * input.y;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;

        animator.SetFloat(SpeedHash, movement.magnitude);
    }
}
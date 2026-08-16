using UnityEngine;
using UnityEngine.InputSystem;

public class Pig : MonoBehaviour
{
    public bool IsCaptured { get; private set; }

    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInput playerInput;

    private Transform _mount;

    public void Capture(Transform mount)
    {
        if (IsCaptured) return;
        IsCaptured = true;
        _mount = mount;

        controller.enabled = false;
        transform.SetParent(mount, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        playerInput.SwitchCurrentActionMap("Captured");
        Debug.Log(mount.lossyScale);
    }

    public void Rescue()
    {
        if (!IsCaptured) return;
        IsCaptured = false;

        transform.SetParent(null, true);
        transform.position = _mount.position + Vector3.up * 1.5f;
        transform.rotation = Quaternion.identity;
        controller.enabled = true;

        playerInput.SwitchCurrentActionMap("Gameplay");
        _mount = null;
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactables : MonoBehaviour
{
    [SerializeField] private float _objectRotateSensitivity = 0.1f;
    private bool _rotatingObject;
    private CameraMovement _cameraMovement;

    private bool _equipped = false;
    private GameObject _equippedObject;
    private float _cooldown;


    private void Start()
    {
        _cameraMovement = Camera.main.GetComponent<CameraMovement>();

    }

    public void RotateObject(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rotatingObject = true;
            _cameraMovement.DisableLook = true;
        }
        else if (context.canceled)
        {
            _cameraMovement.DisableLook = false;
            _rotatingObject = false;
        }
    }

    public void Interact()
    {
        if(_cooldown <= 0)
        {
            if (!_equipped)
            {
                Equip();
                _cooldown = .5f;
            }
            else
            {
                Unequip();
                _cooldown = .5f;
            }
        }
    }


    private void Equip()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                _equippedObject = hit.collider.gameObject;
                _equippedObject.GetComponent<Collider>().isTrigger = true;
                _equippedObject.GetComponent<Rigidbody>().isKinematic = true;
                _equippedObject.transform.SetParent(Camera.main.transform);

                FixedJoint[] joints = _equippedObject.GetComponents<FixedJoint>();
                foreach (FixedJoint joint in joints)
                {
                    Destroy(joint);
                }

                _equipped = true;
            }
        }

    }

    private void Unequip()
    {
        _equippedObject.GetComponent<Collider>().isTrigger = false;
        _equippedObject.GetComponent<Rigidbody>().isKinematic = false;
        _equippedObject.transform.parent = null;
        _equipped = false;
        _equippedObject = null;
    }

    private void Update()
    {
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }

        if (_rotatingObject && _equippedObject != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();


            // Yaw around the camera's up axis, pitch around the camera's right axis
            _equippedObject.transform.Rotate(Camera.main.transform.up, delta.x * _objectRotateSensitivity, Space.World);
            _equippedObject.transform.Rotate(Camera.main.transform.right, -delta.y * _objectRotateSensitivity, Space.World);
        }
    }

    public void Weld(InputAction.CallbackContext context)
    {
        if (_equippedObject == null) return;

        List<Collider> colliders = _equippedObject.GetComponent<ObjectTriggerManager>().Colliders;

        foreach (Collider collider in colliders)
        {
            _equippedObject.AddComponent<FixedJoint>().connectedBody = collider.attachedRigidbody;
        }

        Unequip();
    }
}

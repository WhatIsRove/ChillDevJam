using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mouse / Aim")]
    public float sensitivity;
    public Vector2 verticalRotationLimit;

    GameObject camera;
    Vector2 mousePos;
    Vector2 rotation;


    [Header("Movement")]
    public float moveSpeed;

    Vector3 moveDir;
    Vector3 moveInput;
    Rigidbody rb;

    [Header("Grab")]
    public InputActionReference grabAction;
    public float grabDistance;
    public LayerMask grabLayer;
    public Transform grabPoint;

    bool isGrabbing = false;
    RaycastHit grabHit;
    Grabbable grabbableObj;
    

    void Start()
    {
        camera = Camera.main.transform.gameObject;
        rotation = new Vector2(0, 0);

        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rotation.x -= mousePos.y * sensitivity;
        rotation.y += mousePos.x * sensitivity;

        rotation.x = Mathf.Clamp(rotation.x, verticalRotationLimit.x, verticalRotationLimit.y);

        camera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        grabAction.action.performed += _ => { isGrabbing = true; };
        grabAction.action.canceled += _ => { isGrabbing = false; };

        if (isGrabbing)
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out grabHit, grabDistance, grabLayer) && grabbableObj == null)
            {
                if (grabHit.transform.TryGetComponent(out grabbableObj))
                {
                    grabbableObj.Grab(grabPoint);
                }
            }
        } else
        {
            if (grabbableObj != null)
            {
                grabbableObj.Drop();
                grabbableObj = null;
            }
        }
    }

    private void FixedUpdate()
    {
        moveDir = transform.forward * moveInput.z + transform.right * moveInput.x;

        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (velocity.magnitude > moveSpeed)
        {
            var limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void OnLook(InputValue input)
    {
        mousePos = input.Get<Vector2>();
    }

    void OnMove(InputValue input)
    {
        var temp = input.Get<Vector2>();
        moveInput = new Vector3(temp.x, 0, temp.y);
    }
}

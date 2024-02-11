using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    Rigidbody rb;
    Transform grabPoint;

    public float grabSpeed = 10f;

    public bool grabbed = false;

    public bool facePlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (grabPoint != null)
        {
            Vector3 targetPos = grabPoint.position - transform.position;
            rb.velocity = new Vector3(targetPos.x, targetPos.y, targetPos.z) * grabSpeed;

            if (facePlayer)
            {
                var targetRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position, transform.up);
                rb.rotation = targetRotation;
            }
        }
    }

    public void Grab(Transform grabPoint)
    {
        this.grabPoint = grabPoint;
        rb.useGravity = false;
        rb.drag = 5;
        grabbed = true;
    }

    public void Drop()
    {
        grabPoint = null;
        rb.useGravity = true;
        rb.drag = 0;
        grabbed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    Rigidbody rb;
    Transform grabPoint;

    public float lerpSpeed = 10f;

    public bool grabbed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (grabPoint != null)
        {
            Vector3 lerpPosition = Vector3.Lerp(transform.position, grabPoint.position, Time.fixedDeltaTime * lerpSpeed);
            rb.MovePosition(lerpPosition);
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
        this.grabPoint = null;
        rb.useGravity = true;
        rb.drag = 0;
        grabbed = false;
    }
}

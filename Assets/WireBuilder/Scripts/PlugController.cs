using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlugController : MonoBehaviour
{
    public bool isConected = false;
    public UnityEvent OnWirePlugged;
    public Transform plugPosition;

    [HideInInspector]
    public Transform endAnchor;
    [HideInInspector]
    public Rigidbody endAnchorRB;
    [HideInInspector]
    public Grabbable endAnchorGrabObj;
    [HideInInspector]
    public WireController wireController;
    
    bool hasPlug = false;
    public bool wasPlugged = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug" && !hasPlug && !wasPlugged)
        {
            isConected = true;
            endAnchor = other.transform;
            endAnchorRB = other.GetComponent<Rigidbody>();
            endAnchorGrabObj = other.GetComponent<Grabbable>();

            endAnchorGrabObj.Drop();

            endAnchorRB.isKinematic = true;
            endAnchor.position = plugPosition.position;
            endAnchor.rotation = transform.rotation;

            hasPlug = true;
            OnPlugged();
        }
    }

    private void Update()
    {

        if (isConected)
        {
            endAnchorRB.isKinematic = true;
            endAnchor.transform.position = plugPosition.position;
            Vector3 eulerRotation = new Vector3(this.transform.eulerAngles.x + 90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);

            if (endAnchorGrabObj.grabbed && !wasPlugged)
            {
                isConected = false;
                endAnchorRB.isKinematic = false;
                hasPlug = false;
                wasPlugged = true;
            }
        }

        if (Vector3.Distance(transform.position, endAnchor.position) > 0.5f && wasPlugged)
        {
            wasPlugged = false;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void OnPlugged()
    {
        OnWirePlugged.Invoke();
    }
}

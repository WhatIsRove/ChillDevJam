using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float replugDistance = 0.5f;

    [Header("Puzzle")]
    public bool isPuzzle = false;
    public int currentPlug;

    [Header("Overload")]
    public bool isOverload = false;
    public GameObject vfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug" && !hasPlug && !wasPlugged)
        {
            isConected = true;
            endAnchor = other.transform;
            endAnchorRB = other.GetComponent<Rigidbody>();
            endAnchorGrabObj = other.GetComponent<Grabbable>();

            if (endAnchorGrabObj != null) endAnchorGrabObj.Drop();

            endAnchorRB.isKinematic = true;
            endAnchor.position = plugPosition.position;
            endAnchor.rotation = transform.rotation;

            hasPlug = true;
            if (isPuzzle) GameObject.FindObjectOfType<PlugPuzzle>().CheckPlug(endAnchor, currentPlug);
            if (isOverload) GameObject.FindObjectOfType<OverloadSystem>().UnOverload();
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

            if (endAnchorGrabObj != null)
            {
                if (endAnchorGrabObj.grabbed && !wasPlugged)
                {
                    isConected = false;
                    endAnchorRB.isKinematic = false;
                    hasPlug = false;
                    wasPlugged = true;
                    if (isOverload) GameObject.FindObjectOfType<OverloadSystem>().OverloadEverything(false);
                }
            }
            
        }

        if (endAnchor != null)
        {
            if (Vector3.Distance(transform.position, endAnchor.position) > replugDistance && wasPlugged)
            {
                if (isPuzzle) GameObject.FindObjectOfType<PlugPuzzle>().UnPlug(currentPlug);
                wasPlugged = false;
            }
        }
    }

    public void OnPlugged()
    {
        OnWirePlugged.Invoke();
    }

    public void Yeet()
    {
        isConected = false;
        endAnchorRB.isKinematic = false;
        hasPlug = false;
        wasPlugged = true;
        endAnchorRB.AddForce(transform.up * 50f, ForceMode.Impulse);
        vfx.GetComponent<ParticleSystem>().Play();
    }
}

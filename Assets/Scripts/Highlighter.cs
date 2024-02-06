using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public float highlightDistance;
    public LayerMask interactableLayer;

    Transform camera;
    RaycastHit hit;
    GameObject hitObject;
    bool hitting;

    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(camera.position, camera.forward, out hit, highlightDistance, interactableLayer))
        {
            GameObject obj = hit.transform.gameObject;
            if (hitObject == null)
            {
                //OnHitEnter
                obj.GetComponent<Highlight>().highlight = true;
            } else if (hitObject.GetInstanceID() == obj.GetInstanceID())
            {
                //OnHitStay
                hitObject.GetComponent<Highlight>().highlight = true;
            } else
            {
                //OnHitExit + new HitEnter
                hitObject.GetComponent<Highlight>().highlight = false;
            }

            hitting = true;
            hitObject = obj;
        }
        else
        {
            if (hitting)
            {
                //No object OnHitExit
                if (hitObject != null) hitObject.GetComponent<Highlight>().highlight = false;
                hitting = false;
                hitObject = null;
            }
        }
    }
}

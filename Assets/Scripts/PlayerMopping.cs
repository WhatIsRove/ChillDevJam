using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMopping : MonoBehaviour
{

    public LayerMask moppingLayer;
    public LayerMask lightSwitchLayer;
    private bool isMopping = false;
    private bool isCoroutineRunning = false;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse button
        {
            isMopping = true;
            CheckLightSwitch();
            StartCoroutine(MopRepeatedly());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMopping = false;
            
            StopCoroutine(MopRepeatedly());
        }
    }

    IEnumerator MopRepeatedly()
    {
        
        if (isMopping)
        {
            if (!isCoroutineRunning)
            {
                isCoroutineRunning = true; // Set the flag to indicate that the coroutine is running
                RaycastHit hit;
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 50, moppingLayer))
                {
                    Puddle puddle = hit.transform.gameObject.GetComponent<Puddle>();
                    if (puddle != null)
                    {
                        Mop(puddle);
                    }
                }
                yield return new WaitForSeconds(1f); // Wait for 1 second before repeating
            }
            
        }
        isCoroutineRunning = false; // Reset the flag when the coroutine finishes
    }

    void Mop(Puddle puddle)
    {
        // Code to mop or whatever action you want to perform
        Debug.Log("Mopping...");
        puddle.ReducePuddle();
    }

    private void CheckLightSwitch()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 50, lightSwitchLayer))
        {
            
            LightSwitch.Instance.FlipSwitch();
        }
    }




}

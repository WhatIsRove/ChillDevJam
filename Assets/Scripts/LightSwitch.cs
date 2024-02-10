using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public static LightSwitch Instance;
    private GameObject[] lightObjects;
    private GameObject onSwitch;
    private GameObject offSwitch;
    private List<Light> lights = new List<Light>();

    private bool On;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        onSwitch = GameObject.FindWithTag("OnSwitch");
        offSwitch = GameObject.FindWithTag("OffSwitch");
        onSwitch.SetActive(false);
        lightObjects = GameObject.FindGameObjectsWithTag("Lights");
        foreach (GameObject light in lightObjects)
        {
            lights.Add(light.GetComponentInChildren<Light>());
        }
    }

   

    public void FlipSwitch()
    {
        if (On)
        {

            onSwitch.SetActive(true); 
            offSwitch.SetActive(false);
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
        }
        else
        {
            onSwitch.SetActive(false); 
            offSwitch.SetActive(true);
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
        }

        On = !On;
    }
}

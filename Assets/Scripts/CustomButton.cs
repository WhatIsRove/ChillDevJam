using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour
{
    public UnityEvent buttonPressed;

    public bool state = false;

    private void Start()
    {
        
    }

    public void Press()
    {
        GetComponent<Animator>().SetTrigger("Pressed");

        buttonPressed.Invoke();
    }

    public void SetColor()
    {
        if (state)
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
        } else
        {
            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
        }
        
    }
}

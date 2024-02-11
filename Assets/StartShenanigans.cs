using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShenanigans : MonoBehaviour
{
    public GameObject alarm;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CommenceShenanigans()
    {
        alarm.GetComponent<Animator>().SetBool("Alarm", true);
    }
}

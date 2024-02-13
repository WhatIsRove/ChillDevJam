using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMopping : MonoBehaviour
{

    public LayerMask moppingLayer;
   

  

    void Mop(Puddle puddle)
    {
        // Code to mop or whatever action you want to perform
        Debug.Log("Mopping...");
        puddle.ReducePuddle();
    }


    void OnTriggerEnter(Collider other)
    {
        
           
        if (other.gameObject.layer == LayerMask.NameToLayer("Puddle"))
        {
            Puddle puddle = other.gameObject.GetComponent<Puddle>();
            if (puddle != null)
            {
                Mop(puddle);
            }
        }
 
    }

    void OnTriggerExit(Collider other)
    {
        
    }





}

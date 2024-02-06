using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering.Universal.Internal;

public class Highlight : MonoBehaviour
{
    public bool highlight = false;
    Material[] materials;

    void Start()
    {
        materials = GetComponent<MeshRenderer>().materials;
    }

    void Update()
    {
        if (highlight)
        {
            materials[1].SetFloat("_Scale", 1.04f);
            GetComponent<MeshRenderer>().materials = materials;
        } else
        {
            materials[1].SetFloat("_Scale", 1f);
        }

        GetComponent<MeshRenderer>().materials = materials;
    }
}

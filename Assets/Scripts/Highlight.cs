using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Rendering.Universal.Internal;

public class Highlight : MonoBehaviour
{
    public bool highlight = false;
    Material[] materials;

    public bool isPlug;
    List<Material[]> childrenMaterials = new List<Material[]>();

    void Start()
    {
        if (!isPlug)
        {
            materials = GetComponent<MeshRenderer>().materials;
        } else
        {
            foreach (MeshRenderer meshRenderer in transform.GetChild(0).GetComponentsInChildren<MeshRenderer>())
            {
                childrenMaterials.Add(meshRenderer.materials);
            }
        }
        
    }

    void Update()
    {
        if (!isPlug)
        {
            if (highlight)
            {
                materials[1].SetFloat("_Scale", 1.05f);
            }
            else
            {
                materials[1].SetFloat("_Scale", 1f);
            }

            GetComponent<MeshRenderer>().materials = materials;
        } else
        {
            if (highlight)
            {
                foreach (Material[] materials in childrenMaterials)
                {
                    materials[1].SetFloat("_Scale", 1.05f);
                }

            }
            else
            {
                foreach (Material[] materials in childrenMaterials)
                {
                    materials[1].SetFloat("_Scale", 1f);
                }
            }

            for (int i = 0; i < childrenMaterials.Count; i++)
            {
                transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().materials = childrenMaterials[i];
            }
        }
    }
}

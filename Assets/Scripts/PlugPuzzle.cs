using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlugPuzzle : MonoBehaviour
{
    public MeshRenderer[] plugColors;
    public MeshRenderer[] socketColors;
    public Color[] colors;

    public bool p1 = false;
    public bool p2 = false;
    public bool p3 = false;
    public bool p4 = false;

    public GameObject completionLight;

    [HideInInspector]
    public bool isOverloaded = false;

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color colorHDR;

    void Start()
    {
        List<int> randomPlugs = new List<int>();
        List<int> randomSockets = new List<int>();

        int plugsToAssign = 4;
        while (plugsToAssign > 0)
        {
            int random = Random.Range(0, 4);

            if (!randomPlugs.Contains(random))
            {
                randomPlugs.Add(random);
                plugsToAssign--;
            }
        }

        int socketsToAssign = 4;
        while (socketsToAssign > 0)
        {
            int random = Random.Range(0, 4);

            if (!randomSockets.Contains(random))
            {
                randomSockets.Add(random);
                socketsToAssign--;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            plugColors[randomPlugs[i]].material.color = colors[i];
            socketColors[randomSockets[i]].material.color = colors[i];
        }

    }

    public void CheckPlug(Transform go, int i)
    {
        Color plug = go.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material.color;
        for (int j = 0; j < colors.Length; j++)
        {
            if (plug == colors[j])
            {
                if (j == 0)
                {
                    plug = colors[3];
                } else
                {
                    plug = colors[j - 1];
                }
                break;
            }
        }

        if (plug == socketColors[i-1].material.color)
        {
            switch (i)
            {
                case 1:
                    p1 = true;
                    break;
                case 2:
                    p2 = true;
                    break;
                case 3:
                    p3 = true;
                    break;
                case 4:
                    p4 = true;
                    break;
            }
        }
    }

    public void CheckColors()
    {
        if (isOverloaded) return;
        if (!p1 || !p2 || !p3 || !p4)
        {
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", new Color32(255,0,0,240));
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", colorHDR);
            completionLight.transform.GetChild(2).GetComponent<Light>().color = Color.red;
        } else
        {
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", new Color32(0, 255, 0, 240));
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color32(0, 191, 0, 1));
            completionLight.transform.GetChild(2).GetComponent<Light>().color = Color.green;
            completionLight.GetComponent<AudioSource>().Play();
        }
    }

    public void UnPlug(int i)
    {
        switch (i)
        {
            case 1:
                p1 = false;
                break;
            case 2:
                p2 = false;
                break;
            case 3:
                p3 = false;
                break;
            case 4:
                p4 = false;
                break;
        }
        CheckColors();
    }
}

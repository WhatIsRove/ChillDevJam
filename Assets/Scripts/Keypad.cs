using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public TextMeshProUGUI kpText;
    public GameObject completionLight;

    string randomCode;
    public bool puzzleCompleted = false;
    [HideInInspector]
    public bool isOverloaded = false;

    public GameObject[] clues;

    void Start()
    {
        randomCode = Random.Range(0, 999).ToString("000");
        //kpText.text = randomCode;

        for (int i = 0; i < clues.Length; i++)
        {
            clues[i].SetActive(false);
        }

        for (int i = 0; i < clues.Length; i++)
        {
            if (randomCode.Contains(i.ToString()))
            {
                clues[i].SetActive(true);
            }
        }
        
    }

    public void KeypadPress(int i)
    {
        if (puzzleCompleted) return;
        if (isOverloaded) return;
        if (kpText.text.Length >= 3 && i < 10) return;

        switch (i)
        {
            case 0:
                kpText.text += "0";
                break;
            case 1:
                kpText.text += "1";
                break;
            case 2:
                kpText.text += "2";
                break;
            case 3:
                kpText.text += "3";
                break;
            case 4:
                kpText.text += "4";
                break;
            case 5:
                kpText.text += "5";
                break;
            case 6:
                kpText.text += "6";
                break;
            case 7:
                kpText.text += "7";
                break;
            case 8:
                kpText.text += "8";
                break;
            case 9:
                kpText.text += "9";
                break;
            case 10:
                if (kpText.text.Length <= 0) break;
                kpText.text = kpText.text.Remove(kpText.text.Length-1);
                break;
            case 11:
                if (kpText.text == randomCode)
                {
                    completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", new Color32(0, 255, 0, 240));
                    completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color32(0, 191, 0, 1));
                    completionLight.transform.GetChild(2).GetComponent<Light>().color = Color.green;
                    completionLight.GetComponent<AudioSource>().Play();
                    puzzleCompleted = true;
                } else
                {
                    kpText.text = "";
                }
                break;
        }
    }
}

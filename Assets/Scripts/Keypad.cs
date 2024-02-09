using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public TextMeshProUGUI kpText;
    public GameObject completionLight;

    int randomCode;
    bool puzzleCompleted = false;

    void Start()
    {
        randomCode = Random.Range(0, 999);
        kpText.text = randomCode.ToString("000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeypadPress(int i)
    {
        if (puzzleCompleted) return;
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
                if (kpText.text == randomCode.ToString())
                {
                    completionLight.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
                    puzzleCompleted = true;
                } else
                {
                    kpText.text = "";
                }
                break;
        }
    }
}

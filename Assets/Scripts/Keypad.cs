using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public TextMeshProUGUI kpText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeypadPress(int i)
    {
        switch (i)
        {
            case 0:
                if (kpText.text.Length >= 4) break;
                kpText.text += "0";
                break;
            case 1:
                if (kpText.text.Length >= 4) break;
                kpText.text += "1";
                break;
            case 2:
                if (kpText.text.Length >= 4) break;
                kpText.text += "2";
                break;
            case 3:
                if (kpText.text.Length >= 4) break;
                kpText.text += "3";
                break;
            case 4:
                if (kpText.text.Length >= 4) break;
                kpText.text += "4";
                break;
            case 5:
                if (kpText.text.Length >= 4) break;
                kpText.text += "5";
                break;
            case 6:
                if (kpText.text.Length >= 4) break;
                kpText.text += "6";
                break;
            case 7:
                if (kpText.text.Length >= 4) break;
                kpText.text += "7";
                break;
            case 8:
                if (kpText.text.Length >= 4) break;
                kpText.text += "8";
                break;
            case 9:
                if (kpText.text.Length >= 4) break;
                kpText.text += "9";
                break;
            case 10:
                if (kpText.text.Length <= 0) break;
                kpText.text = kpText.text.Remove(kpText.text.Length-1);
                break;
        }
    }
}

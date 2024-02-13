using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverloadSystem : MonoBehaviour
{
    public Slider slider;
    public float startOverloadValue = 0.1f;
    float currentOverloadValue;
    public RectMask2D uiMask;
    float maxUpMask;
    float initialUpMask;

    public float overloadSpeed = 1f;

    public PlugController socket;

    bool wasOverloaded = false;

    public MeshRenderer[] completionLights;
    public TextMeshProUGUI keypadText;

    public MomoPuzzle buttonPuzzle;

    bool voiced = false;

    void Start()
    {
        currentOverloadValue = startOverloadValue;

        maxUpMask = uiMask.rectTransform.rect.height - uiMask.padding.w - uiMask.padding.y;
        initialUpMask = uiMask.padding.w;
    }

    void Update()
    {
        if (currentOverloadValue >= 100 && !wasOverloaded)
        {
            OverloadEverything(true);
        }

        if (currentOverloadValue < 100 && GetComponent<StartShenanigans>().hasStarted) currentOverloadValue += overloadSpeed * Time.deltaTime;

        var targetHeight = currentOverloadValue * maxUpMask / 100;
        var newUpMask = (maxUpMask + initialUpMask - targetHeight) * 0.5617638f; //multiply by scale hardcoded
        var padding = uiMask.padding;

        padding.w = newUpMask;
        uiMask.padding = padding;
    }

    public void OverloadEverything(bool yeet)
    {
        currentOverloadValue = 100f;
        if (yeet)
        {
            socket.Yeet();
            if (!voiced)
            {
                StartCoroutine(GetComponent<StartShenanigans>().Voices(2));
                voiced = true;
            }
            
        }
        GameObject.FindObjectOfType<Keypad>().isOverloaded = true;
        buttonPuzzle.isOverloaded = true;
        GameObject.FindObjectOfType<PlugPuzzle>().isOverloaded = true;

        wasOverloaded = true;
    }

    public void UnOverload()
    {
        currentOverloadValue = startOverloadValue;

        GameObject.FindObjectOfType<Keypad>().isOverloaded = false;
        buttonPuzzle.isOverloaded = false;
        GameObject.FindObjectOfType<PlugPuzzle>().isOverloaded = false;

        wasOverloaded = false;
    }


}

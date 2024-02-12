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

    public float overloadSpeed = 1f;

    public PlugController socket;

    bool wasOverloaded = false;

    public MeshRenderer[] completionLights;
    public TextMeshProUGUI keypadText;

    public MomoPuzzle buttonPuzzle;

    void Start()
    {
        currentOverloadValue = startOverloadValue;
        slider.value = currentOverloadValue;
    }

    void Update()
    {
        if (currentOverloadValue >= 100 && !wasOverloaded)
        {
            OverloadEverything(true);
        }

        if (currentOverloadValue < 100) currentOverloadValue += overloadSpeed * Time.deltaTime;

        slider.value = currentOverloadValue;
    }

    public void OverloadEverything(bool yeet)
    {
        currentOverloadValue = 100f;
        if (yeet) socket.Yeet();
        GameObject.FindObjectOfType<Keypad>().isOverloaded = true;
        GameObject.FindObjectOfType<MomoPuzzle>().isOverloaded = true;
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

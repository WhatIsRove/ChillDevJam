using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StartShenanigans : MonoBehaviour
{
    public GameObject alarm;

    bool hasStarted = false;

    public Keypad keypadPuzzle;
    public MomoPuzzle buttonPuzzle;
    public PlugPuzzle plugPuzzle;

    bool hasContinued = false;

    bool hasEnded = false;

    public TextMeshProUGUI timerText;
    public float timerDuration = 5f;
    public float currentTime;
    float minutes;
    float seconds;
    float milliseconds;

    public Image panelMaterial;

    bool hasFailed = false;

    private void Start()
    {
        currentTime = timerDuration * 60;
    }

    private void Update()
    {
        if (keypadPuzzle.puzzleCompleted && buttonPuzzle.puzzleCompleted && plugPuzzle.puzzleCompleted && !hasContinued)
        {
            GetComponent<Animator>().SetTrigger("Continue");
            hasContinued = true;
        }

        if (currentTime > 0 && hasStarted)
        {
            currentTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);
            milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);
            
            if (currentTime >= 0) timerText.text = "T-" + minutes.ToString() + ":" + seconds.ToString("00") + "." + milliseconds.ToString("000");
        }

        if (currentTime <= 0 && !hasFailed)
        {
            timerText.text = "T-0:00.000";
            EndShenanigans(2);
            hasFailed = true;
        }
    }

    public void CommenceShenanigans()
    {
        if (!hasStarted)
        {
            alarm.GetComponent<Animator>().SetBool("Alarm", true);
            GetComponent<Animator>().SetTrigger("StartTransform");
            panelMaterial.material.EnableKeyword("_EMISSION");
            timerText.gameObject.SetActive(true);
            GetComponent<OverloadSystem>().slider.gameObject.SetActive(true);
            hasStarted = true;
        }
    }

    public void EndShenanigans(int i)
    {
        if (!hasEnded)
        {
            switch (i)
            {
                case 0:
                    //Abort ending
                    alarm.GetComponent<Animator>().SetBool("Alarm", false);
                    GetComponent<OverloadSystem>().overloadSpeed = 0;
                    hasStarted = false;
                    break;
                case 1:
                    GetComponent<OverloadSystem>().overloadSpeed = 0;
                    hasStarted = false;
                    //Divert power ending
                    break;
                case 2:
                    //Run out of time
                    GetComponent<OverloadSystem>().overloadSpeed = 0;
                    break;
            }

            hasEnded = true;
        }
        
    }
}

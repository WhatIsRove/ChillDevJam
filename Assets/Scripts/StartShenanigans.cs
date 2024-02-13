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

    public bool hasStarted = false;

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

    public GameObject compVoice;
    AudioSource[] compVoices;
    bool sixtySec = false;

    public GameObject generalVoice;
    AudioSource[] generalVoices;

    public GameObject gameImage;

    private void Start()
    {
        currentTime = timerDuration * 60;
        compVoices = compVoice.GetComponents<AudioSource>();
        generalVoices = generalVoice.GetComponents<AudioSource>();
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

        
        if (currentTime <= 60 && !sixtySec)
        {
            StartCoroutine(Voices(1));
            sixtySec = true;
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
            StartCoroutine(Voices(0));
            GetComponent<OverloadSystem>().slider.gameObject.SetActive(true);
            hasStarted = true;
        }
    }

    public IEnumerator Voices(int i)
    {
        switch (i)
        {
            case 0:
                //start
                compVoices[0].Play();
                yield return new WaitForSecondsRealtime(1.5f);
                generalVoices[0].Play();
                yield return new WaitForSecondsRealtime(5.4f);
                generalVoices[1].Play();
                yield return new WaitForSecondsRealtime(4.6f);
                generalVoices[2].Play();
                break;
            case 1:
                //hurry up
                compVoices[2].Play();
                yield return new WaitForSecondsRealtime(1.5f);
                generalVoices[3].Play();
                break;
            case 2:
                //overload
                generalVoices[4].Play();
                yield return new WaitForSecondsRealtime(1.5f);
                break;
            case 3:
                //clue to wires
                generalVoices[5].Play();
                yield return new WaitForSecondsRealtime(1.5f);
                break;
            case 4:
                //failure
                compVoices[3].Play();
                yield return new WaitForSecondsRealtime(2.74f);
                compVoices[5].Play();
                yield return new WaitForSecondsRealtime(0.26f);
                generalVoices[6].Play();
                yield return new WaitForSecondsRealtime(4.89f);
                FindObjectOfType<AudioManager>().Play("Missile");
                break;
            case 5:
                //abort
                compVoices[1].Play();
                yield return new WaitForSecondsRealtime(1.9f);
                generalVoices[7].Play();
                break;
            case 6:
                //Divert power
                compVoices[4].Play();
                yield return new WaitForSecondsRealtime(4.8f);
                generalVoices[8].Play();
                yield return new WaitForSecondsRealtime(3.5f);
                compVoices[5].Play();
                yield return new WaitForSecondsRealtime(5.15f);
                FindObjectOfType<AudioManager>().Play("Missile");
                yield return new WaitForSecondsRealtime(0.7f);
                generalVoices[6].Play();
                break;
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
                    StartCoroutine(Voices(5));
                    hasStarted = false;
                    break;
                case 1:
                    //Divert power ending
                    GetComponent<OverloadSystem>().overloadSpeed = 0;
                    gameImage.SetActive(true);
                    StartCoroutine(Voices(6));
                    hasStarted = false;
                    break;
                case 2:
                    //Run out of time
                    GetComponent<OverloadSystem>().overloadSpeed = 0;
                    StartCoroutine(Voices(4));
                    break;
            }

            hasEnded = true;
        }
        
    }
}

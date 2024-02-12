using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StartShenanigans : MonoBehaviour
{
    public GameObject alarm;

    bool hasStarted = false;

    public Keypad keypadPuzzle;
    public MomoPuzzle buttonPuzzle;
    public PlugPuzzle plugPuzzle;

    bool hasContinued = false;

    bool hasEnded = false;

    private void Update()
    {
        if (keypadPuzzle.puzzleCompleted && buttonPuzzle.puzzleCompleted && plugPuzzle.puzzleCompleted && !hasContinued)
        {
            GetComponent<Animator>().SetTrigger("Continue");
            hasContinued = true;
        }
    }

    public void CommenceShenanigans()
    {
        if (!hasStarted)
        {
            alarm.GetComponent<Animator>().SetBool("Alarm", true);
            GetComponent<Animator>().SetTrigger("StartTransform");

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
                    break;
                case 1:
                    //Divert power ending
                    break;
                case 2:
                    //Run out of time
                    break;
            }

            hasEnded = true;
        }
        
    }
}

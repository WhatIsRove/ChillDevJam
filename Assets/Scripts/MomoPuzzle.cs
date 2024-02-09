using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MomoPuzzle : MonoBehaviour
{
    public GameObject[] buttons;

    public GameObject completionLight;

    bool puzzleCompleted = false;

    List<int> buttonNums = new List<int>();

    public void AlternateButtons(int i)
    {
        if (puzzleCompleted) return;

        switch (i)
        {
            case 1:
                buttonNums.Add(2);
                buttonNums.Add(3);
                break;
            case 2:
                buttonNums.Add(1);
                buttonNums.Add(4);
                break;
            case 3:
                buttonNums.Add(1);
                buttonNums.Add(4);
                buttonNums.Add(5);
                break;
            case 4:
                buttonNums.Add(2);
                buttonNums.Add(3);
                buttonNums.Add(6);
                break;
            case 5:
                buttonNums.Add(3);
                buttonNums.Add(6);
                break;
            case 6:
                buttonNums.Add(4);
                buttonNums.Add(5);
                break;
        }

        foreach (int j in buttonNums)
        {
            var currentButton = buttons[j - 1].GetComponent<CustomButton>();

            if (currentButton.state)
            {
                currentButton.state = false;
                currentButton.SetColor();
            } else if (!currentButton.state)
            {
                currentButton.state = true;
                currentButton.SetColor();
            }
        }

        bool allDone = false;
        foreach (GameObject go in buttons)
        {
            if (!go.GetComponent<CustomButton>().state)
            {
                allDone = false;
                break;
            } else
            {
                allDone = true;
            }
        }
        if (allDone)
        {
            completionLight.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.green);
            puzzleCompleted = true;
        }
            

        buttonNums.Clear();
    }
}

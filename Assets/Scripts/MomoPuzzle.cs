using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MomoPuzzle : MonoBehaviour
{
    public GameObject[] buttons;

    public GameObject completionLight;

    public bool puzzleCompleted = false;

    List<int> buttonNums = new List<int>();

    [HideInInspector]
    public bool isOverloaded = false;

    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0:
                    button.GetComponent<MeshRenderer>().material.color = Color.red;
                    button.GetComponent<CustomButton>().state = false;
                    break;
                case 1:
                    button.GetComponent<MeshRenderer>().material.color = Color.green;
                    button.GetComponent<CustomButton>().state = true;
                    break;
            }
        }

        buttons[0].GetComponent<MeshRenderer>().material.color = Color.red;
        buttons[0].GetComponent<CustomButton>().state = false;
    }

    public void AlternateButtons(int i)
    {
        if (puzzleCompleted) return;
        if (isOverloaded) return;

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
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_BaseColor", new Color32(0, 255, 0, 240));
            completionLight.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color32(0, 191, 0, 1));
            completionLight.transform.GetChild(2).GetComponent<Light>().color = Color.green;
            completionLight.GetComponent<AudioSource>().Play();
            puzzleCompleted = true;
        }
            

        buttonNums.Clear();
    }
}

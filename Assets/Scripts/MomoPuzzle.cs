using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MomoPuzzle : MonoBehaviour
{
    public GameObject[] buttons;

    public Material red;
    public Material green;

    List<int> buttonNums = new List<int>();

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void One()
    {
        buttonNums.Add(2);
        buttonNums.Add(3);
        AlternateButtons(buttonNums);
    }

    public void Two()
    {
        buttonNums.Add(1);
        buttonNums.Add(4);
        AlternateButtons(buttonNums);
    }

    public void Three()
    {
        buttonNums.Add(1);
        buttonNums.Add(4);
        buttonNums.Add(5);
        AlternateButtons(buttonNums);
    }

    public void Four()
    {
        buttonNums.Add(2);
        buttonNums.Add(3);
        buttonNums.Add(6);
        AlternateButtons(buttonNums);
    }

    public void Five()
    {
        buttonNums.Add(3);
        buttonNums.Add(6);
        AlternateButtons(buttonNums);
    }

    public void Six()
    {
        buttonNums.Add(4);
        buttonNums.Add(5);
        AlternateButtons(buttonNums);
    }

    void AlternateButtons(List<int> buttonNum)
    {
        foreach (int i in buttonNum)
        {
            var currentButton = buttons[i - 1].GetComponent<CustomButton>();

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


        buttonNums.Clear();
    }
}

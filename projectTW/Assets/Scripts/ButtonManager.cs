using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [HideInInspector] public Button previousClicked;
    [HideInInspector] public int globalIndex = -1;
    public Button lastClicked;
    public Button[] clickedButtons;
    public Button[] allButtons;
    [SerializeField] LineRenderer _lineRenderer;
    
    private void Start()
    {
        allButtons = this.gameObject.GetComponentsInChildren<Button>();

        clickedButtons = new Button[allButtons.Length];

        foreach(Button button in allButtons)
        {
            if(button.isStartButton == true)
            {
                clickedButtons[0] = button;
                globalIndex += 1;
            }
        }
    }
    private void Update() 
    {
        PreviousClickedToClickedButtons();

        LastClickedButton();

        DrawLine();

        if(globalIndex > 2 && lastClicked.isFinishButton == true)
        {
            if(CheckIfWin() == false)
            {
                ResetGame();
            }
            else
            {
                Debug.Log("you won");
            }
        }
        
    }

    private void PreviousClickedToClickedButtons()
    {
        if(globalIndex > -1)
         {
            if(previousClicked != null)
            {
                clickedButtons[globalIndex] = previousClicked;
            }

            previousClicked = null;
        }
    }

    private void ResetGame()
    {
        lastClicked.isFinishButton = false;

        lastClicked.isStartButton = true;

       for(int i=1 ; i < globalIndex; i++ )
       {
           clickedButtons[i].buttonState = false;
           clickedButtons[i] = null;
       }

       globalIndex = 0;
    }

    private void LastClickedButton()
    {
        if(globalIndex > -1)
        {
            lastClicked = clickedButtons[globalIndex];
        }
        else
        {
            lastClicked = null;
        }
    }

    private bool CheckIfWin()
    {
            foreach(Button button in allButtons)
            {
                if(button.orderIndex == -1 && button.buttonState == true && button.isFinishButton == false)
                {
                    return false;
                }
                else if(button.orderIndex > -1 && button.buttonState == false && button.isFinishButton == false)
                {
                    return false;
                }
                else if(button.orderIndex > -1 && button.buttonState == true && button.orderIndex != button.currentIndex && button.isFinishButton == false)
                {
                    return false;
                }
            }
            return true;
    }

    private void DrawLine()
    {
        _lineRenderer.positionCount = globalIndex + 1;

        for(int i = 0; i< _lineRenderer.positionCount; i++ )
        {
            Vector3 cube = clickedButtons[i].GetComponent<Transform>().position;

            _lineRenderer.SetPosition(i,new Vector3(cube.x,cube.y,-16));
        }

        LineRendererEnableLoop();
    }

    private void LineRendererEnableLoop()
    {
        if(globalIndex > 2)
        {
            _lineRenderer.loop = lastClicked.isFinishButton;
        }
    }
}

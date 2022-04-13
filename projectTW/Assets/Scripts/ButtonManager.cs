using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [HideInInspector] public Button previousClicked;
    [HideInInspector] public bool resetGame = false;
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

        Debug.Log("Win Status: " +CheckIfWin());

        DrawLine();

        LineRendererEnableLoop();

        if (resetGame == true)
        {
            ResetGame();    
        }
        else if(clickedButtons[0] == null)
        {
            ResetGame();
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
        resetGame = false;

        globalIndex = -1;

        for(int i=0; i<clickedButtons.Length; i++)
        {
            if(clickedButtons[i] != null)
            {
                clickedButtons[i].buttonState = false;
                clickedButtons[i] = null;
            }
        }
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
        if(globalIndex > -1 && lastClicked.isFinishButton == true)
        {
            foreach(Button button in allButtons)
            {
                if(button.orderIndex == -1 && button.buttonState == true)
                {
                    return false;
                }
                else if(button.orderIndex > -1 && button.buttonState == false)
                {
                    return false;
                }
                else if(button.orderIndex > -1 && button.buttonState == true && button.orderIndex != button.currentIndex)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    private void DrawLine()
    {
        _lineRenderer.positionCount = globalIndex + 1;

        for(int i = 0; i< _lineRenderer.positionCount; i++ )
        {
            Vector3 cube = clickedButtons[i].GetComponent<Transform>().position;

            _lineRenderer.SetPosition(i,new Vector3(cube.x,cube.y,-16));
        }
    }

    private void LineRendererEnableLoop()
    {
        if(globalIndex > -1)
        {
            _lineRenderer.loop = lastClicked.isFinishButton;
        }
    }
}

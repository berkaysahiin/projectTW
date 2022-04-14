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

        Debug.Log("Win status: " + CheckIfWin());
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
        if(CheckIfLoopIsCompleted() == true)
        {
            foreach(Button button in allButtons)
            {
                if(button.isStartButton == false)
                {
                    if(button.orderIndex == -1 && button.currentIndex > -1)
                    {
                        return false;
                    }

                    else if(button.orderIndex > -1 && button.currentIndex == -1)
                    {   
                        return false;
                    }

                    else if(button.orderIndex > -1 && button.orderIndex > -1 && button.orderIndex != button.currentIndex )
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }

    }

    private void DrawLine()
    {
        _lineRenderer.positionCount = globalIndex + 1;

        if(globalIndex > 1)
        {
            for(int i = 0; i< _lineRenderer.positionCount; i++ )
        {
            Vector3 cube = clickedButtons[i].GetComponent<Transform>().position;

            Vector3 cubeLocal = cube - transform.forward;

            _lineRenderer.SetPosition(i,new Vector3(cubeLocal.x, cubeLocal.y, cubeLocal.z));
        }
        }

        LineRendererEnableLoop();
    }

    private void LineRendererEnableLoop()
    {
        if(globalIndex > 2)
        {
            _lineRenderer.loop = lastClicked.isStartButton;
        }
    }

    private bool CheckIfLoopIsCompleted()
    {
        if(globalIndex >= 3 && lastClicked.isStartButton == true )
        {
            Debug.Log("loop is over");
            return true;
        }
        else
        {
            return false;
        }
    }
}

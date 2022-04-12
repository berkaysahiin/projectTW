using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ButtonTest[] buttons;
    public ButtonTest[] clickedButtons;
    public ButtonTest previousClicked;
    public ButtonTest lastClicked;
    public bool resetGame = false;
    public bool win = false;

    public int myIndex = -1;

    private void Update() 
    {
        ClickedButtonManager();

        CheckLastButton();
        
        DrawLine();   

        CheckIfWin();

        if (resetGame == true)
        {
            ResetGame();    
        }
        else if(clickedButtons[0] == null)
        {
            ResetGame();
        }
        
    }

    private int LineRendererPositionCount()
    {
        lineRenderer.positionCount = myIndex + 1;

        return lineRenderer.positionCount;
    }

    private void DrawLine()
    {
        for(int i=0; i< LineRendererPositionCount(); i++)
        {
            lineRenderer.SetPosition(i, clickedButtons[i].GetComponentInParent<Transform>().position);
        }  
    }

    private void ClickedButtonManager()
    {
        if(myIndex > -1)
         {
            if(previousClicked != null)
            {
                clickedButtons[myIndex] = previousClicked;
            }

            previousClicked = null;
        }
    }

    private void ResetGame()
    {
        resetGame = false;

        myIndex = -1;

        for(int i=0; i<clickedButtons.Length; i++)
        {
            if(clickedButtons[i] != null)
            {
                clickedButtons[i].buttonState = false;
                clickedButtons[i] = null;
            }
        }
    }

    private void CheckLastButton()
    {
        if(myIndex > -1)
        {
            lastClicked = clickedButtons[myIndex];
        }
        else
        {
            lastClicked = null;
        }
    }

    private void CheckIfWin()
    {
        if(myIndex > -1)
        {
            if(clickedButtons[myIndex].isFinishButton == true)
            {
                for(int i =0; i< myIndex; i++)
                {
                    if(clickedButtons[i].orderIndex == -1)
                    {
                        resetGame = true;
                    }

                    else if(clickedButtons[i].orderIndex != clickedButtons[i].thisIndex)
                    {
                        resetGame = true;
                        
                    }
                }
            }
        }
    }
}

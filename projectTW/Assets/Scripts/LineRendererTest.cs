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

    public int myIndex = -1;

    private void Update() 
    {
        

        LineRendererPositionCountManager();

        ClickedButtonManager();

        DrawLine();   

        ResetGame();    
        
    }

    private void LineRendererPositionCountManager()
    {
        lineRenderer.positionCount = myIndex + 1;
    }

    private void DrawLine()
    {
        for(int i=0; i<lineRenderer.positionCount; i++)
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
            
            if(previousClicked != null) 
            {
                lastClicked = previousClicked;
            }

            previousClicked = null;
        }
    }

    private void ResetGame()
    {
        if(resetGame == true)
        {
            resetGame = false;

            lastClicked = null;

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
    }

}

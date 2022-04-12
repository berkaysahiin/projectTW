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
    private bool win = false;
    [SerializeField] LineRenderer _lineRenderer;
    
    private void Update() 
    {
        PreviousClickedToClickedButtons();

        LastClickedButton();

        CheckIfWin();

        DrawLine();

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

    private void CheckIfWin()
    {
        if(globalIndex > -1)
        {
            if(clickedButtons[globalIndex].isFinishButton == true)
            {
                for(int i =0; i< globalIndex; i++)
                {
                    if(clickedButtons[i].orderIndex == -1)
                    {
                        resetGame = true;
                        return;
                    }

                    else if(clickedButtons[i].orderIndex != clickedButtons[i].currentIndex)
                    {
                        resetGame = true;
                        return;
                        
                    }
                }

                win = true;

                return;
            }
            else if(clickedButtons[globalIndex].isFinishButton == false)
            {
                win = false;
            }
        }
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
}

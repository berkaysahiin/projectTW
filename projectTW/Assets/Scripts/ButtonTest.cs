using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTest : MonoBehaviour
{
    public bool buttonState = false;
    public LineRendererTest lineRenderer;
    [HideInInspector] public int thisIndex;
    public int orderIndex;
    public int gridIndexX;
    public int gridIndexY;
    [HideInInspector] public bool mouseHold;
    [HideInInspector] public bool mouseOver;
    public bool isFinishButton = false;


    private void Update()
    {
        if(buttonState == true)
        {
            var renderer = this.GetComponent<Renderer>();

            renderer.material.SetColor("_Color",Color.red);
        }
        else if(buttonState == false)
        {
            var renderer = this.GetComponent<Renderer>();

            renderer.material.SetColor("_Color",Color.grey);

            thisIndex = -1;
        }

        if(Input.GetMouseButtonDown(0))
        {
            mouseHold = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseHold = false;
        }
    }
    
    private void OnMouseEnter() 
    {
        if(mouseHold)
        {
            if(CheckCanClick())
            {
                ClickButton();
            }
        }
    }

    private void OnMouseExit()
    {
        
    }

    private void OnMouseDown()
    {
        
        if(CheckCanClick())
        {
            ClickButton();
        }
        
    }

    private void ClickButton()
    {
        if(buttonState == false)
        {
            buttonState = true;

            lineRenderer.myIndex += 1;

            thisIndex = lineRenderer.myIndex;

            lineRenderer.previousClicked = gameObject.GetComponent<ButtonTest>();
        } 
        else if(buttonState == true)
        {

            if(lineRenderer.myIndex == thisIndex)
            {
                lineRenderer.clickedButtons[lineRenderer.myIndex].buttonState = false;

                lineRenderer.clickedButtons[lineRenderer.myIndex] = null;

                lineRenderer.myIndex -= 1;
            }
        }
    } 

    public bool CheckCanClick()
    {
        if(lineRenderer.myIndex == -1) // check if it's the first grid.
        {
            if(orderIndex != 0)
            {
                return false;
            } 
        }


        if(buttonState == true)
        {
            return true;

        }
        else
        {
            if(lineRenderer.lastClicked == null)
            {
                return true;
            }
            else
            {
                if(Mathf.Abs(lineRenderer.lastClicked.gridIndexY - gridIndexY) == 0)
                {
                    if(Mathf.Abs(lineRenderer.lastClicked.gridIndexX - gridIndexX) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else if(Mathf.Abs(lineRenderer.lastClicked.gridIndexY - gridIndexY) == 1)
                {
                    if(Mathf.Abs(lineRenderer.lastClicked.gridIndexX - gridIndexX) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }
        }
    }

     public void CheckOrder()
    {
        if(thisIndex > -1)
        {
            if(orderIndex == thisIndex)
            {
                Debug.Log("true");
            }
            else
            {
                Debug.Log("false");
                // reset game
                lineRenderer.resetGame = true;
                thisIndex = -1;
            }
        }
    }
}

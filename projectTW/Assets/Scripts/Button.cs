using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public bool buttonState = false;
    public ButtonManager buttonManager;
    [HideInInspector] public int currentIndex;
    public int orderIndex;
    public int gridIndexX;
    public int gridIndexY;
    [HideInInspector] public bool mouseHold;
    public bool isFinishButton = false;
    public bool isCircled = false;
    public bool isWall;
    
    private void Start()
    {
        buttonManager = FindObjectOfType<ButtonManager>();
    }

    void Update()
    {
        IndexOfButtonIfNonClicked();

        CheckMouseHold();
        
        WallColorManager();
    }
    
    private void OnMouseEnter() 
    {
        if(mouseHold == true)
        {
            if(CheckCanClick() == true)
            {
                ClickButton();
            }
        }
    }

    private void OnMouseDown()
    {
        if(CheckCanClick() == true)
        {
            ClickButton();
        }
    }

    private void ClickButton()
    {
        if(buttonState == false)
        {
            buttonState = true;

            buttonManager.globalIndex += 1;

            currentIndex = buttonManager.globalIndex;

            buttonManager.previousClicked = gameObject.GetComponent<Button>();
        } 
        else if(buttonState == true)
        {

            if(buttonManager.globalIndex == currentIndex)
            {
                buttonManager.clickedButtons[buttonManager.globalIndex].buttonState = false;

                buttonManager.clickedButtons[buttonManager.globalIndex] = null;

                buttonManager.globalIndex -= 1;
            }
        }
    } 

    public bool CheckCanClick()
    {
        if(buttonManager.globalIndex == -1) // check if it's the first grid.
        {
            if(orderIndex != 0)
            {
                return false;
            } 
        }

        if(CheckCanClickEveryTurnGameModeAddition() == false)
        {
            return false;
        }
        
        if(CheckCanClickWallAddition() == false)
        {
            return false;
        }

        if(buttonState == true)
        {
            return true;

        }
        else
        {
            if(buttonManager.lastClicked == null)
            {
                return true;
            }
            else
            {
                if(Mathf.Abs(buttonManager.lastClicked.gridIndexY - gridIndexY) == 0)
                {
                    if(Mathf.Abs(buttonManager.lastClicked.gridIndexX - gridIndexX) == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else if(Mathf.Abs(buttonManager.lastClicked.gridIndexY - gridIndexY) == 1)
                {
                    if(Mathf.Abs(buttonManager.lastClicked.gridIndexX - gridIndexX) == 0)
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

    private void IndexOfButtonIfNonClicked()
    {
        if(buttonState == false)
        {
            currentIndex = -1;
        }
    }

    private void ColorOfButton()
    {
       if(isCircled == false)
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
            }
       }

       else
       {
           if(buttonState == false)
            {
                var renderer = this.GetComponent<Renderer>();

                renderer.material.SetColor("_Color",Color.yellow);
            }
            else if(buttonState == true)
            {
                var renderer = this.GetComponent<Renderer>();

                renderer.material.SetColor("_Color",Color.green);
            }
       }
    }

    private void CheckMouseHold()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseHold = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseHold = false;
        }
    }

    private bool CheckCanClickEveryTurnGameModeAddition()
    {
        if(buttonManager.globalIndex > 0 && buttonState == false)
        {
            if(buttonManager.lastClicked.isCircled == true)
            {
                if(buttonManager.lastClicked.gridIndexX == buttonManager.clickedButtons[buttonManager.globalIndex -1].gridIndexX)
                {
                    if(gridIndexY != buttonManager.lastClicked.gridIndexY)
                    {
                        return false;
                    }
                }

                else if(buttonManager.lastClicked.gridIndexY == buttonManager.clickedButtons[buttonManager.globalIndex -1].gridIndexY)
                {
                    if(gridIndexY == buttonManager.lastClicked.gridIndexY)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void WallColorManager()
    {
        if(isWall == true)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color",Color.black);
        }
    }

    private bool CheckCanClickWallAddition()
    {
        if(isWall == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

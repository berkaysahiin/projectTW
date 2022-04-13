using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public ButtonManager buttonManager;

    [HideInInspector] public bool mouseHoldRightClick;
    [HideInInspector] public bool mouseHoldLeftClick;
    public bool buttonState = false;
    public bool isStartButton = false;
    public bool isFinishButton = false;
    public bool isCircled = false;
    public bool isWall;
    
    public int currentIndex = -1;
    public int gridIndexX;
    public int gridIndexY;
    public int orderIndex = -1;
    
    
    private void Start()
    {
        buttonManager = FindObjectOfType<ButtonManager>();

        
    }

    void Update()
    {
        mouseHoldLeftClick = Input.GetMouseButton(0);
        mouseHoldRightClick = Input.GetMouseButton(1);

        IndexOfButtonIfNonClicked();

        CheckMouseHold();
        
        WallColorManager();

        CircleColorManager();

        CircleAndWallAtTheSameTimeController();

        StartAndFinishButtonColorManager();

        Debug.Log(CheckCanClickLeftRightClickAddition());
        
    }
    
    private void OnMouseEnter() 
    {
        if(CheckCanClick() && CheckMouseHold())
        {
            ClickButton();
        }
    }

    private void OnMouseOver()
    {
        if( CheckCanClick() && CheckCanClickLeftRightClickAddition())
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

        if(CheckCanClickWallAddition() == false)
        {
            return false;
        }

        if(isStartButton == true)
        {
            return false;
        }

        if(CheckCanClickCircleAddition() == false)
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
    private bool CheckMouseHold()
    {

        if(mouseHoldLeftClick)
        {
            if(buttonState == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else if(mouseHoldRightClick)
        {
            if(buttonState == true)
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

    private bool CheckCanClickCircleAddition()
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

    private void CircleColorManager()
    {
        if(isCircled == true)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color",Color.yellow);

            Debug.Log(this.gameObject.name);
        }
    }
    

    private void CircleAndWallAtTheSameTimeController()
    {
        if(isCircled == true && isWall == true)
        {
            Debug.Log("error: Circle and wall at the same time");
        }
    }

    private void StartAndFinishButtonColorManager()
    {
        if(isFinishButton == true)
        {
            var renderer = this.GetComponent<Renderer>();

            renderer.material.SetColor("_Color",Color.blue);
        }

        if(isStartButton == true)
        {
            var renderer = this.GetComponent<Renderer>();
            renderer.material.SetColor("_Color",Color.magenta);
        }
    }

    private void FirstButtonManager()
    {
        if(isStartButton == true)
        {
            buttonManager.globalIndex += 1;
            buttonManager.clickedButtons[buttonManager.globalIndex + 1] = gameObject.GetComponent<Button>();

        }
    }

    private bool CheckCanClickLeftRightClickAddition()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(buttonState == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if(buttonState == true)
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

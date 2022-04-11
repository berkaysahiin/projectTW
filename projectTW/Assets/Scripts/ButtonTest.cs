using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTest : MonoBehaviour
{
    public bool buttonState = false;
    public LineRendererTest lineRenderer;
    public int thisIndex;
    public int orderIndex;


    private void Update()
    {
        if(buttonState == true)
        {
            transform.localScale = new Vector3(1,2,1);
        }
        else if(buttonState == false)
        {
            transform.localScale = new Vector3(1,1,1);

            thisIndex = -1;
        }
    }
    
    private void OnMouseDown() 
    {
        ClickButton();
        CheckOrder();
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
            lineRenderer.clickedButtons[lineRenderer.myIndex].buttonState = false;

            lineRenderer.clickedButtons[lineRenderer.myIndex] = null;

            lineRenderer.myIndex -= 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTest : MonoBehaviour
{
    public bool buttonState = false;
    public LineRendererTest lineRenderer;
    private void OnMouseDown() 
    {
        ClickButton();
    }

    private void ClickButton()
    {
        if(buttonState == false)
        {
            buttonState = true;
            lineRenderer.myIndex += 1;
            lineRenderer.previousClicked = gameObject.GetComponent<ButtonTest>();
        } 
        else
        {
            lineRenderer.clickedButtons[lineRenderer.myIndex].buttonState = false;
            lineRenderer.clickedButtons[lineRenderer.myIndex] = null;
            lineRenderer.myIndex -= 1;
        }
    }  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTest : MonoBehaviour
{
    public bool buttonState = false;
    public LineRendererTest _lineRenderer;
    private void OnMouseDown() 
    {
        ClickButton();
    }

    private void ClickButton()
    {
       

        if(buttonState == false)
        {
             _lineRenderer.myIndex += 1;

             _lineRenderer.previousClicked = gameObject.GetComponent<ButtonTest>();
           
        } 
        else
        {
            _lineRenderer.clickedButtons[_lineRenderer.myIndex] = null;

            _lineRenderer.myIndex -= 1;

            Debug.Log("i worked");
            
        }

        buttonState = !buttonState;

        
    }  
}

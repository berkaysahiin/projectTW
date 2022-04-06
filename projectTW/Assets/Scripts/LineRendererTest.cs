using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ButtonTest[] buttons;
    public ButtonTest[] clickedButtons;
    public ButtonTest previousClicked;

    public int myIndex = -1;

    private void Update() 
    {
        lineRenderer.positionCount = myIndex + 1;

        if(myIndex > -1)
         {
            if(previousClicked != null)
            {
                clickedButtons[myIndex] = previousClicked;
            }
            
            previousClicked = null;
        }  

        for(int i=0; i<lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, clickedButtons[i].GetComponentInParent<Transform>().position);
        }

        Debug.Log(previousClicked);


        Debug.Log(myIndex);
    }

}

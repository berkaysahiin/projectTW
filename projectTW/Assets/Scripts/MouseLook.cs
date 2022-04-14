using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    public bool puzzleMode;

    [SerializeField] private float xRotation;
    [SerializeField] private float mounseSensivty;  
    private float mouseX;
    private float mouseY;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Update()
    {
        GetMouseInput();

        if(PuzzleModeInAndOutManagar() == false)
        {
            FPSController();
            LockCursor();

            
       
        }
        else
        {
            ReleaseCursor();

            //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            
        }
        
    }

    private void GetMouseInput()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mounseSensivty;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mounseSensivty;
    }

    private bool PuzzleModeInAndOutManagar()
    {
        if(Input.GetKey(KeyCode.E))
        {
            puzzleMode = true;
        }
        else
        {
            puzzleMode = false;
        }

        return puzzleMode;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void FPSController()
    {
        playerBody.Rotate(Vector3.up * mouseX);
            
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
    }
}

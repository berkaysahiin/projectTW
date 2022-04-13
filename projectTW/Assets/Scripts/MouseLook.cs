using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    

    [SerializeField] private float mounseSensivty;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float xRotation;

    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    void Update()
    {
        

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mounseSensivty;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mounseSensivty;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed = 12f;
    bool isGrounded;
    Vector3 velocity;

    // Update is called once per frame
    void Update() 
    {
        CharacterController controller = transform.GetComponent<CharacterController>();

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (isGrounded && velocity.y < 0) 
        { 
            velocity.y = -2f; 
        }
        
        if (isGrounded && Input.GetButtonDown("Jump")) 
        { 
            velocity.y = 10.85f; 
        
        }

        velocity.y -= 19.62f * Time.deltaTime;

        controller.Move((transform.right * Input.GetAxis("Horizontal") * speed + transform.up * velocity.y + transform.forward * Input.GetAxis("Vertical") * speed) * Time.deltaTime);
    }
}
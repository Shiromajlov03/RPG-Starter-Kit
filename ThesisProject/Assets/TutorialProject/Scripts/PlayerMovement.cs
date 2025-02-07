using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody characterRB;
    private Vector3 movementInput;
    private Vector3 movementVector;
    private float movementSpeed = 150;
    [SerializeField] float JumpHeight;
    [SerializeField] bool isGrounded;

    public bool actionInput { get; private set; }
    



    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        applyMovement();
    }

    private void OnMovement(InputValue input)
    {
        movementInput = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }
    private void OnMovementStop(InputValue input)
    {
        movementVector = Vector3.zero;
    }

    private void OnRunning()
    {
        movementSpeed = movementSpeed * 2;
    }
    private void OnRunningStop()
    {
        movementSpeed = movementSpeed / 2;
    }
    private void OnCrouching()
    {
        movementSpeed = movementSpeed / 2;
    }
    private void OnCrouchingStop()
    {
        movementSpeed = movementSpeed * 2;
    }
    private void applyMovement()
    {
        if (movementInput != Vector3.zero)
        {
            // Calculate movement vector based on input and current orientation of the character
            movementVector = transform.right * movementInput.x + transform.forward * movementInput.z;
        }

        Vector3 newVelocity = movementVector * movementSpeed;
        newVelocity.y = characterRB.velocity.y;
        // Set the velocity of the character's Rigidbody to move it
        characterRB.velocity = (newVelocity * Time.fixedDeltaTime);
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if we landed on a surface tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnJump(InputValue input)
    {
        if (isGrounded) { 
        characterRB.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
        isGrounded = false; // Temporarily set false until we land
        }
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float forwardMoveSpeed = 6f;
    [SerializeField] private float backwardMoveSpeed = 3.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float totalStamina = 100f;
    [SerializeField] private float currentStamina = 100f;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool canRun = true;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * horizontalMovement + transform.forward * verticalMovement;

        float moveSpeedToUse = (verticalMovement >= 0) ? forwardMoveSpeed : backwardMoveSpeed;


        if (canRun)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentStamina > 0) 
                { 
                    isRunning = true;
                    forwardMoveSpeed = 12f;
                }

            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0)
            {
                StartCoroutine(RunningCooldown());
                isRunning = false;
                forwardMoveSpeed = 6f;
            }
        }

        if (isRunning)
        {
            currentStamina -= 30 * Time.deltaTime;
        }
        else
        {
            if (currentStamina < totalStamina)
            {
                currentStamina += 15 * Time.deltaTime;
            }
        }

        characterController.Move(movement * moveSpeedToUse * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }

    IEnumerator RunningCooldown()
    {
        canRun = false;
        yield return new WaitForSeconds(2.5f);
        if (currentStamina > 0)
        {
            canRun = true;
        }
    }

}

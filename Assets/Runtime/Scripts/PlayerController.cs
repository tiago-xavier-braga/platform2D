using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 400;
    [SerializeField] private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerInputActions inputActions;
    public GameObject player;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
        animator= GetComponent<Animator>();
        inputActions.PlayerControls.Enable();
        inputActions.PlayerControls.Jump.performed += onJumpInput;
    }

    private void onJumpInput(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
    }

    private void Update()
    {
        if (moveInput.x < 0)
        {
            if (player.transform.rotation.y == 0)
            {
                player.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        if (moveInput.x > 0)
        {
            player.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(moveInput.x != 0) {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRun", false);
        }

        moveInput = inputActions.PlayerControls.Movement.ReadValue<Vector2>();
        rb.velocity = moveInput * speed * Time.deltaTime;
    }
}

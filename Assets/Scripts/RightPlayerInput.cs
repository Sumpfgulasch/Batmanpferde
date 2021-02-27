using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightPlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool enterKey;
    [HideInInspector] public float enterKeyPressTime;

    public float moveSpeed = 2f;

    Rigidbody rb;
    private Coroutine holdPunchRoutine;
    private float holdPunchTime;

    void Start()
    {
        moveInput = new Vector2();
        rb = this.GetComponent<Rigidbody>();
    }

    

    public void GetInput()
    {
        // Move
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            moveInput.y = 1;
        }
        else if (Keyboard.current.upArrowKey.wasReleasedThisFrame)
        {
            moveInput.y = 0;
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            moveInput.y = -1;
        }
        else if (Keyboard.current.downArrowKey.wasReleasedThisFrame)
        {
            moveInput.y = -1;
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            moveInput.x = 1;
        }
        else if (Keyboard.current.rightArrowKey.wasReleasedThisFrame)
        {
            moveInput.x = 0;
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            moveInput.y = -1;
        }
        else if (Keyboard.current.leftArrowKey.wasReleasedThisFrame)
        {
            moveInput.y = 0;
        }

        rb.velocity = moveInput;

        // Enter
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            enterKey = true;
            enterKeyPressTime = 0;
            StartCoroutine(EnterPressTime());
        }
        if (Keyboard.current.enterKey.wasReleasedThisFrame)
        {
            enterKey = false;
            enterKeyPressTime = 0;
            StopCoroutine(EnterPressTime());
        }

        
    }


    private IEnumerator EnterPressTime()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            enterKeyPressTime = timer;

            yield return null;
        }
    }
}

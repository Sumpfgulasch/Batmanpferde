using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightPlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool fickDich;
    [HideInInspector] public float fickDichPressTime;
    void Start()
    {
        moveInput = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInput()
    {
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


        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            fickDich = true;
        }
        if (Keyboard.current.enterKey.wasReleasedThisFrame)
        {
            fickDich = false;
            fickDichPressTime = 0;
        }

        
    }


    private IEnumerator FickDichPressTime()
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            fickDichPressTime = timer;

            yield return null;
        }
    }
}

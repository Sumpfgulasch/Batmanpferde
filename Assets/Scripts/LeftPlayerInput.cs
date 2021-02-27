using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftPlayerInput : MonoBehaviour
{
    public float moveSpeed = 2f;

    Rigidbody rb;
    private Coroutine holdPunchRoutine;
    private float holdPunchTime;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    


    // -------------------------------- Events --------------------------------



    public void OnMove(InputAction.CallbackContext context)
    {
        // Setze velocity
        if (context.performed)
        {
            var input = context.ReadValue<Vector2>();
            rb.velocity = new Vector3(input.x, 0, input.y) * moveSpeed;
        }
        else if (context.canceled)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        // Zähle hoch
        if (context.performed)
        {
            holdPunchRoutine = StartCoroutine(CountHoldTime());
        }

        // Raise event
        else if (context.canceled)
        {
            GameEvents.inst.onPunch?.Invoke(holdPunchTime);
            StopCoroutine(holdPunchRoutine);
        }
    }


    public IEnumerator CountHoldTime()
    {
        holdPunchTime = 0;

        while (true)
        {
            holdPunchTime += Time.deltaTime;

            yield return null;
        }
    }
}

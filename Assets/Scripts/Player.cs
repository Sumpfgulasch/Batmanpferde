using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public Player otherPlayer
	{
		get
		{
			// this is retarded
			Player[] players = FindObjectsOfType<Player>();

			for (int i = 0; i < players.Length; i++)
			{
				if (players[i] != this)
				{
					return players[i];
				}
			}
			
			return null;

		}
	}
	public Collider BallHitZone;
    public float moveSpeed = 2f;


    private Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            print("a is pressed");
        }
        //if (Keyboard.)
        //print("k value: " + Keyboard.current.kKey.ReadValue());
    }

	public void HitBall(float force, Vector2 hitDirection)
	{
        
	}



    // -------------------------------- Events --------------------------------



    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var value = context.ReadValue<Vector2>();
            var velocity = new Vector3(value.x, 0, value.y);

            rb.velocity = velocity * moveSpeed;

            print("move");
        }
        else if (context.canceled)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void OnHit(InputAction.CallbackContext context)
    {

    }

    //public void OnMove()
    //{
    //    print("on move broadcast");
    //}


}

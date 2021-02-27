using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	//public static Player inst;
	public Player otherPlayer
	{
		get
		{
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
    [HideInInspector] public Vector2 moveInput;
    

    [HideInInspector] public float holdPunchTime;

    private Rigidbody rb;
    private Coroutine holdPunchRoutine; 
	
    
    void Start()
    {
        moveInput = new Vector2();
        rb = this.GetComponentInChildren<Rigidbody>();
    }

    
    void Update()
    {
        //rb.velocity = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
    }




	public void HitBall(float force, Vector2 hitDirection)
	{
        
	}



    


}

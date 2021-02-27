using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Ball : MonoBehaviour
{
	public float curMoveSpeed;

	public AnimationCurve heightToTime;
	public float maxHeight;


	public AnimationCurve distanceToForce;

	public LineRenderer lineRenderer;

	// temp
	public Vector2 direction;
	public float distance;

	[Header("Following The Trajectory")]
	public Trajectory curTrajectory;
	Vector3 targetPoint;
	bool followTarget;
	float accuracy = 0.1f;
	public AnimationCurve speedToHeight;
	public int targetPointIndex = 0;
	public float maxSpeed = 1;

	// events

	System.Action<Player> OnBallTouchedGround;
	System.Action<Player> OnPlayerHitBall;

	public Player currentBallOwner; 

	public void Awake()
	{
		// wire events

		OnBallTouchedGround += (Player ballOwner) =>
		{
			MoveBallToPosition(ballOwner.otherPlayer.transform.position + new Vector3(0f, 0.5f, 0f));
			followTarget = false;
			targetPointIndex = 0;
		};
	}


	public void Update()
	{
		if (followTarget)
		{
			FollowTrajectory(curTrajectory, true);
		}
	}
	public bool isHittableBy(Player player)
	{
		return false;
	}

	//public Trajectory CalculateTrajectory()
	//{

	//}

	[Button]
	public void InitiateMovementOnCurrentTrajectory()
	{
		targetPointIndex = 0;
		followTarget = true;
	}

	[Button]
	public void CalculateNewTrjectory()
	{
		Trajectory newTrajectory = new Trajectory(this.transform.position, direction, distance, maxHeight, heightToTime, 100);
		newTrajectory.PutIntoLineRenderer(lineRenderer);
		curTrajectory = newTrajectory;
	}



	public void FollowTrajectory(Trajectory trajectory, bool destroyTrajectory = true)
	{
		Debug.Log("Ball fallows rajectory");
		if (curTrajectory.points.Count > 0)
		{
			targetPoint = curTrajectory.points[targetPointIndex];

			// getCurrentSpeedRelativeToHeight 
			float curSpeed = speedToHeight.Evaluate(transform.position.y / maxHeight) * Time.deltaTime * maxSpeed;
			Debug.Log("Ball fallows rajectory. Cur Speed" + curSpeed);

			transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, curSpeed);

			if (Vector3.Distance(transform.position, targetPoint) < accuracy)
			{
				// if we are destroying the trajectory, remove the firsat point from its list
				// the trget index remains 0, because its the next point that has the index 1 now
				if (destroyTrajectory)
				{
					trajectory.points.Remove(targetPoint);
					
					if(trajectory.points.Count == 0)
					{
						OnBallTouchedGround.Invoke(currentBallOwner);
					}
				}

				// if we arenot destroying the trajectory just increase the index
				else
				{
					targetPointIndex++;
				}
			}

		}
	}

	public void MoveBallToPosition(Vector3 position)
	{
		this.transform.position = position;
	}


	private void OnTriggerEnter(Collider other)
	{
		PlayerArea playerArea = other.gameObject.GetComponent<PlayerArea>();
		if (playerArea != null)
		{
			currentBallOwner = playerArea.owner;
		}
	}
}

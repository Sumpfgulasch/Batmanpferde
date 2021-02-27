using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Ball : MonoBehaviour
{
	public float curMoveSpeed;

	public AnimationCurve heightToTime;
	public float maxHeight;

	public AnimationCurve speedToTime;
	public AnimationCurve distanceToForce;

	public LineRenderer lineRenderer;

	// temp
	public Vector2 direction;
	public float distance;

	public bool isHittableBy(Player player)
	{
		return false;
	}

	//public Trajectory CalculateTrajectory()
	//{

	//}

	public  void InitiateMove(Trajectory trajectory)
	{

	}

	[Button]
	public void CalculateTrjectory() 
	{
		Trajectory newTrajectory = new Trajectory(this.transform.position, direction, distance, maxHeight, heightToTime, 50);
		newTrajectory.PutIntoLineRenderer(lineRenderer);

	}





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trajectory
{
	public Vector3 startPoint;
	public Vector3 endPoint;
	public List<Vector3> points;

	public float distance;
	public Vector2 direction;
	public float height;


	public Trajectory(Vector3 startPoint, Vector2 direction, float distance, float height, AnimationCurve HeightCurve, int iterations)
	{
		// save values in the object
		this.startPoint = startPoint;
		this.direction = direction.normalized;
		this.height = height;
		this.distance = distance;

		// calculate the end point
		// we are setting the y to 0, because it should land on the floor

		endPoint = startPoint + new Vector3(direction.normalized.x * distance, 0, direction.normalized.y * distance);
		endPoint.y = 0f;

		Debug.Log("End point" + endPoint);
		// calculate the fraction of height the tragectory was started at
		// if max height is 5.0 and the bal was hit at 1.0, then this fraction will be 0.1.
		// we use this value to determine where on the curve we are starting the trajectory
		float startHeightFraction;

		if (startPoint.y > 0) {
			startHeightFraction =  startPoint.y / height;
		}
		else
		{
			startHeightFraction = 0;
		}

		float startTimeFraction = GetFirstTimeForValue(HeightCurve,startHeightFraction);

		Debug.Log("Start Height Fraction:" + startHeightFraction);
		Debug.Log("Start Time Fraction:" + startTimeFraction);



		// create a list of points

		float evaluationStep = (1f - startTimeFraction) / (float) iterations;
		Debug.Log("Evaluation step" + evaluationStep);

		points = new List<Vector3>();

		// start with the initial point 
		points.Add(startPoint);

		for (int i = 0; i < iterations; i++)
		{
			// lerp position for the x and y component
			Vector3 newPosition = Vector3.Lerp(startPoint, endPoint, i * evaluationStep);

			//  replace y component with height calculated from the graph
			//newPosition.y = 0;
			newPosition.y = HeightCurve.Evaluate(startTimeFraction + evaluationStep * i) * height;

			//  add to list
			points.Add(newPosition);

		}

		points.Add(endPoint);
	}

	public void PutIntoLineRenderer( LineRenderer lineRenderer)
	{
		lineRenderer.positionCount = points.Count;
		Vector3[] pointsArray = points.ToArray();
		lineRenderer.SetPositions(pointsArray);
	}

	public float GetFirstTimeForValue(AnimationCurve curve, float targetValue)
	{
		float step = 0.01f;
		float evaluatedTime = 0f;

		while (evaluatedTime < 1)
		{
			float value = curve.Evaluate(evaluatedTime);

			if(value >= targetValue)
			{
				return evaluatedTime;
			}
			else
			{
				evaluatedTime += step;
			}
		}

		return 0;
	}

	public void UpdateLines()
	{

	}

}

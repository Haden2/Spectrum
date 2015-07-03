using UnityEngine;
using System.Collections;

public class LightsOut : MonoBehaviour {

	//public GameObject[] lights;
	//public GameObject Target;
	public GameObject closest;
	public GameObject[] lights;
	public float distance;
	public Vector3 position;


	void Start () 
	{
		lights = GameObject.FindGameObjectsWithTag ("Light");

		for(int l = 0; l < lights.Length; l++)
		{
			print(lights.Length);
			//GetClosestLight(l);
		}
		distance = Mathf.Infinity;
		position = transform.position;
	}
	
	void Update () 
	{
		foreach (GameObject go in lights) 
		{
			print (closest.name);
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
			}
			print(diff);
			closest = go;
		//	Target.transform.position = closest.transform;
			//return closest;
		}


	}

	/*Transform GetClosestLight (Transform[] lights)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach(Transform potentialTarget in lights)
		{
			Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}
		print (bestTarget);
		return bestTarget;
	}
	GameObject FindClosestLight() 
	{
		GameObject[] lights;
		lights = GameObject.FindGameObjectsWithTag("Light");
		GameObject closest;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in lights) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
		print (closest.name);
	}*/
}

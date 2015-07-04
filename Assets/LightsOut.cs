using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class LightsOut : MonoBehaviour 
{
	public GameObject target;
	public GameObject[] lights;
	public GameObject closest;
	public Light targetLight;
	public float distance;

	void Awake()
	{
		InvokeRepeating("FindClosestPlayer", 0.5f,0.5f);
	}

	void Update()
	{
		if(target != null)
		{
			targetLight = target.gameObject.GetComponent<Light> ();
		}
		//targetLight.intensity = 7;
	} 

	GameObject FindClosestPlayer() 
	{
		lights = GameObject.FindGameObjectsWithTag("Light");
		distance = Mathf.Infinity;
		foreach (GameObject go in lights) 
		{
			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) 
			{
				closest = go;
				distance = curDistance;
			}
		}
		target = closest.gameObject;
		return closest;
	}
}

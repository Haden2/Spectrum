using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

public class LightsOut : MonoBehaviour 
{
	public GameObject target;
	public GameObject[] lights;
	public GameObject closest;
	public Vector3 rightAngle;
	public float targetRange;
	public float targetAngle;
	public bool lightMonsterSeen;
	public bool inLight;
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
			if(targetLight.name == ("Spotlight"))
			{
				targetLight.transform.Rotate(Vector3.up*Time.deltaTime*50);
			}
			if(targetLight.type == LightType.Spot)
			{
				Quaternion rotation = Quaternion.AngleAxis(targetAngle * .5f, targetLight.transform.up);
				Quaternion rotationL = Quaternion.AngleAxis(-targetAngle * .5f, targetLight.transform.up);
				RaycastHit hit;
				RaycastHit rightHit;
				RaycastHit leftHit;
				Vector3 rayDirection = targetLight.transform.position - transform.position;
				Ray ray = new Ray(targetLight.transform.position, targetLight.transform.forward);
				Ray rightRay = new Ray(targetLight.transform.position, rotation * targetLight.transform.forward);
				Ray leftRay = new Ray(targetLight.transform.position, rotationL * targetLight.transform.forward);

				if(Physics.Raycast(ray, out hit, targetRange))
				{
					print (hit.transform);
					if(hit.transform.name == "LightMonster")
					{
						lightMonsterSeen = true;
					}
					if(hit.transform.name != "LightMonster")
					{
						lightMonsterSeen = false;
					}
					Debug.DrawRay(targetLight.transform.position, targetLight.transform.forward * targetRange);
				}
				if(Physics.Raycast(rightRay, out rightHit, targetRange))
				{
					if(rightHit.transform.name == "LightMonster")
					{
						print("Something");
					}
				}
				if ((Vector3.Angle(rayDirection, -targetLight.transform.forward)) <= targetAngle * 0.5f)
				{
					inLight = true;
					Debug.DrawRay(targetLight.transform.position, rotation * targetLight.transform.forward * targetRange, Color.blue);
					Debug.DrawRay(targetLight.transform.position, rotationL * targetLight.transform.forward * targetRange, Color.blue);
				}
				else{
					inLight =false;
				}
			}
			if(targetLight.intensity >=2)
			{

			}
			if(targetLight.intensity <2)
			{

			}
			//rightAngle = Quaternion.AngleAxis(((targetAngle * .5f, targetLight.transform.up) * targetLight.transform.forward));
			targetRange = targetLight.range;
			targetAngle = targetLight.spotAngle;
		}
		//targetLight.color = (Color.blue);
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

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightsOut : MonoBehaviour 
{
	//Have 2 float values. 1 is the distance from the light then a new float is the new distance away. That replaces the first float value. So if 1 is greater than 2, then it is getting closer to the source. 
	//Have a raycast appear inside the bounds of the light that follows the enemy. Figure out the global angles of the raycasts. So once I figure out how to get the global angle of the blue line, then I can subtract the two in order to find the purple angle
	//and green angle is orange angle minus purple angle
	//Hypotenuse * sin(alpha) gives the opposite side length which is the spot from the end of the hypotenuse to the center of the circle. 
	public Material fadeMaterial;
	//MeshRenderer rendererer;
	public Color newColor;
	public Light targetLight;
	public GameObject target;
	public GameObject[] lights;
	public GameObject closest;

	public bool lightMonsterSeen;
	public bool inLight;
	
	public float targetRange;
	public float targetAngle;
	public float distance;
	public float distanceFromLight;
	public float alpha;
	public float hypotenuseAngle;

	public Vector3 rightAngle;

	void Awake()
	{
		//rendererer = gameObject.GetComponent<MeshRenderer> ();
		fadeMaterial = gameObject.GetComponent<Renderer>().material;
		newColor = fadeMaterial.color;
		InvokeRepeating("FindClosestLight", 0.5f,0.5f); //Calls the function to find the nearest light.
	}

	void Update()
	{
		if(target != null) //If the target variable isn't blank. Putting things outside of this if statement creates issues because it takes a couple seconds for the game to find all of the lights.
		{
			targetLight = target.gameObject.GetComponent<Light> (); //grabbed the gameobjects light component.
			if(targetLight.name == ("LMspotlight")) //if that closest light is called spotlight
			{
				targetLight.transform.Rotate(Vector3.up*Time.deltaTime*50); //spin the light around. *50 is the speed. Increase to make it go faster
			}
			if(targetLight.type == LightType.Spot) //if it is a spotlight in general
			{
				Quaternion rotation = Quaternion.AngleAxis(targetAngle * .5f, targetLight.transform.up); //this is the right side of the circle.
				Quaternion rotationL = Quaternion.AngleAxis(-targetAngle * .5f, targetLight.transform.up); //this is the left edge of the circle.
				RaycastHit hit; //dead center spot
				RaycastHit rightHit; //the raycast on the right edge
				RaycastHit leftHit; // the raycast on the left edge
				Vector3 rayDirection = targetLight.transform.position - transform.position; //the distance from the light to the light monster. 
				Ray ray = new Ray(targetLight.transform.position, targetLight.transform.forward); //ray from light to player
				Ray rightRay = new Ray(targetLight.transform.position, rotation * targetLight.transform.forward); //ray from light to right side of circle
				Ray leftRay = new Ray(targetLight.transform.position, rotationL * targetLight.transform.forward); //ray from light to left side of circle
		
				if(Physics.Raycast(ray, out hit, targetRange)) //if the raycast hit something within range. targetRange matches range of spotlight.
				{
				//	print (rayDirection); //the distance from light to light monster in this case is 9. This is the Adjacent leg of the triangle. I will get more into that in github
					//print (hit.transform);   says what I hit
					if(hit.transform.name == "LightMonster") //did the middle ray hit the monster?
					{
						lightMonsterSeen = true; //yeah, monster is seen.
					}
					if(hit.transform.name != "LightMonster") //is it not hitting the monster?
					{
						lightMonsterSeen = false; //monster is not being directly looked at.
					}
					Debug.DrawRay(targetLight.transform.position, targetLight.transform.forward * targetRange); //Draw this beam out.
				}
				if(Physics.Raycast(rightRay, out rightHit, targetRange)) // if the right side of the light hits something
				{
					if(rightHit.transform.name == "LightMonster") //if it hits the monster
					{
						distanceFromLight = rightHit.distance;
						Debug.DrawRay(targetLight.transform.position, rotation * targetLight.transform.forward * targetRange, Color.blue); //show right ray
						Debug.DrawRay(targetLight.transform.position, rotationL * targetLight.transform.forward * targetRange, Color.blue); //show left ray
						if(distanceFromLight >5)
						{
							//print (newColor.a);
							//newColor.a = alpha;
							this.GetComponent<MeshRenderer>().material.color = new Color (1,1,1,.5f);
						}
						if(distanceFromLight < 5)
						{
							this.GetComponent<MeshRenderer>().material.color = new Color (1,1,1,.2f);
						}
					//	print (rightHit.transform.position); // the global position of the light monster
						//print (hit.transform.position); // the global position of the wall behind the light monster
						//rightAngle = rightHit.transform.position - hit.transform.position;   All this did was say that the wall is at this position, and the enemy is at this position. Doesn't give a good value.
					}
				}
				if ((Vector3.Angle(rayDirection, -targetLight.transform.forward)) <= targetAngle * 0.5f) // if the enemy is in the spotlight at all
				{
					inLight = true; //it is in the light.
				}
				else{
					inLight =false; //otherwise, dont show it
				}
			}
			if(targetLight.intensity >=2) //if that light is bright
			{

			}
			if(targetLight.intensity <2) // if that light is dim
			{

			}
			targetRange = targetLight.range;  //the targetRange is how far the light goes. The lights range.
			targetAngle = targetLight.spotAngle; // the targetAngle is how wide the circle is
			hypotenuseAngle = targetAngle/2;
		}
	} 

	GameObject FindClosestLight() //Looks through all of the lights and finds the nearest one.
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

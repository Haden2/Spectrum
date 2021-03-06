﻿using UnityEngine;
using System.Collections;

public class CeilingClimber : MonoBehaviour {

	public GameObject player;
	public GameObject extra;

	public bool foundPlayer;
	public bool onPlayer;
	public bool leftTurn;
	public bool rightTurn;
	public bool secondLeft;
	public bool thirdLeft;
	public bool hold;
	public bool thrownOff;
	public bool restart;
	public bool hunt;

	public float shakeTime;
	public float timeLimit;
	public float endShakeTime;
	public float zombieViewAngle = 100;

	public Vector3 contactPosition; 

	public NavMeshAgent nav;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("First Person Controller");
		extra = GameObject.Find("AdditionalParts");

		hold = true;

		shakeTime = 0;
		timeLimit = .5f;

		nav = GetComponent<NavMeshAgent> ();
		nav.speed = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		endShakeTime = Time.realtimeSinceStartup - shakeTime;

		Vector3 rayDirection = player.transform.position - transform.position;
		//RaycastHit hit;

		if ((Vector3.Angle(rayDirection, transform.forward)) <= zombieViewAngle * 0.5f)
		{
			foundPlayer = true;
		}
		if(foundPlayer)
		{
			nav.SetDestination (player.transform.position);
		}
		if(onPlayer)
		{
			//contactPosition = gameObject.transform.position;
			//gameObject.transform.position = playerPosition;
			
			if(Input.GetAxis("Mouse X")<-5)
			{
				//print (Input.GetAxis("Mouse X"));
				//Code for action on mouse moving left
				if(hold)
				{
					leftTurn = true;
					hold = false;
				}
				if(leftTurn && rightTurn)
				{
					ShakeTime();
					rightTurn = false;
					leftTurn = false;
					secondLeft = true;
				}
				if(secondLeft && rightTurn)
				{
					rightTurn = false;
					secondLeft = false;
					thirdLeft = true;
				}
				if(thirdLeft && rightTurn)
				{
					if((Time.realtimeSinceStartup - shakeTime) < timeLimit)
					{
					thirdLeft = false;
					rightTurn = false;
					thrownOff = true;
					}
					if((Time.realtimeSinceStartup - shakeTime) > timeLimit)
					{
						thirdLeft =false;
						rightTurn = false;
						hold = true;
					}
				}	
				if(thrownOff)
				{
					restart = true;
					print (endShakeTime);
				}
			}
			if(Input.GetAxis("Mouse X")>5)
			{
				//print (Input.GetAxis("Mouse X"));
				//Code for action on mouse moving right
				rightTurn = true;
			}
		}
		if(restart)
		{
			foundPlayer = false;
			onPlayer = false;
			nav.speed = 5;
			nav.acceleration = 10;
			nav.SetDestination(extra.transform.position);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			onPlayer = true;
		}
	}

	void ShakeTime()
	{
		//lastTapTime = Time.realtimeSinceStartup;
		shakeTime = Time.realtimeSinceStartup;
	}
}

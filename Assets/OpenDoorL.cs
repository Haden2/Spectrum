﻿using UnityEngine;
using System.Collections;

public class OpenDoorL : MonoBehaviour {
	
	GameObject key;
	//public BoxCollider button;
	public Transform door;
	public Transform pivot;
	Vector3 Angle;
	Vector3 newAngle;
	bool open;
	bool closed;
	bool haveKey;
	bool rotating;
	bool closing;
	
	void Start () 
	{
		closed = true;
		open = false;
		haveKey = false;
		rotating = false; 
		key = GameObject.FindGameObjectWithTag ("Key");
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == key)
		{
			StartCoroutine (WantToOpenDoor());
		}
	}

	IEnumerator DoorOpens ()
	{
		rotating = true;
		open = true;
		yield return new WaitForSeconds (5);
		rotating = false;
		open = false;
		closing = true;
	}
	
	IEnumerator WantToOpenDoor()
	{
		haveKey = true;
		yield return new WaitForSeconds (0);
	}
	
	void Update () 
	{
		if(closed == true && haveKey == true && Input.GetKeyDown ("e")/*Event.current.button == 0 && Event.current.type == EventType.mouseUp*/)
		{
			StartCoroutine(DoorOpens());
		}
		if(closed == true && haveKey == false)
		{
			//print ("Need Key First");
		}
		if(rotating)
		{
			Vector3 Angle = new Vector3(0,0,0);
			Vector3 newAngle = new Vector3(0,-90,0);
			if(Vector3.Distance(transform.eulerAngles, Angle) > .01f)
			{
				transform.RotateAround(pivot.position, newAngle, .5f);
			}
			else
			{
				transform.eulerAngles = Angle;
				rotating = false;
			}
			
		}
		if(open == false && closing == true)
		{
			print ("closing door");
			Vector3 closingAngle = new Vector3(0,90,0);
			
			if(Vector3.Distance(transform.eulerAngles, closingAngle) > .01f)
			{
				transform.RotateAround(pivot.position, closingAngle, .5f);
			}
			else
			{
				transform.eulerAngles = closingAngle;
				rotating = false;
			}
		}
	}
	
}


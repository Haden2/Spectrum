/*using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	//public Vector3 elevator;
	//public Vector3 downPosition;
	//public Vector3 upPosition;
	//public Vector3 openPosition;
	//public Vector3 closedPosition;
	//public Vector3 openPosition1;
	//public Vector3 closedPosition1;
	//GameObject player;
	//public GameObject bigDoor;
	//public GameObject littleDoor;
	//float allAboard;
	//public bool isDown;
	//public bool isUp;
	//public bool isMoving;
	//public bool canActivate;
	//public bool closeDoor;
	//public bool openDoor;

	void Start()
	{
		//isDown = true;
		//isUp = false;
		//isMoving = false;
		//closeDoor = true;
		//openDoor = false;
		//canActivate = false;
		//allAboard = 4;
		//elevator = transform.localPosition;
		//downPosition = new Vector3(13, 1, -5);
		//upPosition = new Vector3 (13, 15, -5);
		//openPosition = new Vector3 (10, 2.5f, -1);
		//closedPosition = new Vector3 (10, 2.5f, -5);
		//openPosition1 = new Vector3 (9.85f, 2.5f, -1);
		//closedPosition1 = new Vector3 (9.85f, 2.5f, -3);

	//	player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		//if(closeDoor)
		{
			//openDoor = false;
		//	StartCoroutine(ElevatorDoor (bigDoor.transform, openPosition, closedPosition, 4));	
		//	StartCoroutine(ElevatorDoor (littleDoor.transform, openPosition1, closedPosition1, 4));
		}
	//	if(openDoor)
		{
			//closeDoor = false;
		//	StartCoroutine(ElevatorDoor (bigDoor.transform, closedPosition, openPosition, 4));	
		//	StartCoroutine(ElevatorDoor (littleDoor.transform, closedPosition1, openPosition1, 4));		
		}

		if(isDown == true && isUp == false && isMoving == false && canActivate == true && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveUp (transform, downPosition, upPosition, 5));
		}

		if(isDown == false && isUp == true && isMoving == false && canActivate == true && Input.GetKeyDown ("t"))
		{
			StartCoroutine(MoveDown (transform, upPosition, downPosition, 5));
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other == OutsideElevator)
		if(other.gameObject == player)
		{
			print ("Entered Elevator");
			//StartCoroutine (Begin());
			canActivate = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			StartCoroutine (End());
		}
	}
	
	IEnumerator Begin()
	{
		canActivate = true;
		yield return null;
	}

	IEnumerator End()
	{
		canActivate = false;
		yield return null;
	}

	IEnumerator CanMoveUp()
	{
		if (isMoving == true) 
		{
			yield return new WaitForSeconds (5);
			isDown = true;
			isUp = false;
			isMoving = false;
		}
	}
	IEnumerator CanMoveDown()
	{
		if (isMoving == true) 
		{
			yield return new WaitForSeconds (5);
			isDown = false;
			isUp = true;
			isMoving = false;
		}
	}
	

	IEnumerator MoveUp(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		isMoving = true;
		StartCoroutine(CanMoveDown());
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (startPos, endPos, i);
			yield return null;
		}
	}

	IEnumerator MoveDown(Transform thisTransform, Vector3 endPos, Vector3 startPos, float time)
	{
		isMoving = true;
		StartCoroutine(CanMoveUp());
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f) 
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp (endPos, startPos, i);
			yield return null;
		}
	}

	IEnumerator ElevatorDoor(Transform thatTransform, Vector3 startArea, Vector3 endArea, float timing)
	{
		float h = 0.0f;
		float rot = 1.0f / timing;
		while(h< 1.0f)
		{
			h += Time.deltaTime * rot;
			thatTransform.position = Vector3.Lerp (startArea, endArea, h);
			//thatTransform.position = Vector3.Lerp(startArea, endArea, h);
			yield return null;
		}
		//yield return new WaitForSeconds (allAboard);
		stationary = true;
		print("This is when it activates");
	}

	IEnumerator ElevatorDoorClose(Transform thatTransform, Vector3 endArea, Vector3 startArea, float timing)
	{
		float h = 0.0f;
		float rot = 1.0f / timing;
		while(h< 1.0f)
		{
			h += Time.deltaTime * rot;
			thatTransform.position = Vector3.Lerp (endArea, startArea, h);
			//thatTransform.position = Vector3.Lerp(endArea, startArea, h);
			yield return null;
			//stationary = true;
		}
	}
}*/

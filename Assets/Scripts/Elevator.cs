using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Vector3 elevator;
	public Vector3 downPosition;
	public Vector3 upPosition;
	public Vector3 openPosition;
	public Vector3 closedPosition;
	public Vector3 openPosition1;
	public Vector3 closedPosition1;
	GameObject player;
	public GameObject bigDoor;
	public GameObject littleDoor;
	public bool isDown;
	public bool isUp;
	public bool isMoving;
	public bool canActivate;
	public bool closeDoor;


	void Start()
	{
		isDown = true;
		isUp = false;
		isMoving = false;
		closeDoor = true;
		canActivate = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		if(closeDoor)
		{
			StartCoroutine(ElevatorDoor (bigDoor.transform, openPosition, closedPosition, 3));	
			StartCoroutine(ElevatorDoor (littleDoor.transform, openPosition1, closedPosition1, 3));		
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
		if(other.gameObject == player)
		{
			StartCoroutine (Begin());
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
			thatTransform.position = Vector3.Lerp(startArea, endArea, h);
			yield return null;
		}
	}

}

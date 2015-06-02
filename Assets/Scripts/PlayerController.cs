using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject elevator;
	GameObject oldMan;
	Wander wander;
	public Inventory inventory;
	GameObject bigDoor;
	GameObject littleDoor;
	public bool elevatorDoor;
	public bool inElevator;
	public bool doorIsClosed;
	public bool canActive;
	public bool isDown;
	public bool isUp;
	public bool isMoving;
	public bool oldManSeen;
	public bool peripheral;
	public Vector3 downPosition;
	public Vector3 upPosition;
	float power;
	float oldManViewAngle = 60;

	void Start()
	{
		wander = GameObject.FindGameObjectWithTag ("OldMan").GetComponent<Wander> ();
		elevator = GameObject.FindGameObjectWithTag ("Elevator");
		inventory = GetComponent<Inventory> ();
		bigDoor = GameObject.FindGameObjectWithTag ("BigDoor");
		littleDoor = GameObject.FindGameObjectWithTag ("LittleDoor");
		downPosition = new Vector3(13, 1, -5);
		upPosition = new Vector3 (13, 15, -5);
		isDown = true;
		isUp = false;
		isMoving = false;
		oldMan = GameObject.FindGameObjectWithTag ("OldMan");
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.name == "Elevator")
		{
			elevatorDoor = true;
		}
		if(elevatorDoor && inventory.activeElevatorKey)
		{
			canActive = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "Elevator")
		{
		elevatorDoor = false;
		}
	}

	void Update()
	{
		if(wander.attack || wander.newDestination)
		{
			oldManSeen = false;
		}
		if(elevatorDoor && inventory.activeElevatorKey && canActive == true && Input.GetKeyDown("e"))
		{
			littleDoor.GetComponent<Animation>().Play("SmallElevatorDoor");
			bigDoor.GetComponent<Animation>().Play("ElevatorDoor");
		}
		if(isDown == true && isUp == false && isMoving == false && canActive == true && Input.GetKeyDown ("1"))
		{
			StartCoroutine(MoveUp (elevator.transform, downPosition, upPosition, 5));
		}
		if(isDown == false && isUp == true && isMoving == false && canActive == true && Input.GetKeyDown ("1"))
		{
			StartCoroutine(MoveDown (elevator.transform, upPosition, downPosition, 5));
		}
		if(bigDoor.GetComponent<Animation>().isPlaying)
		{
			canActive = false;
		}
		else
		{
			canActive = true;
		}
	
		{
			RaycastHit hit;
			Vector3 rayDirection = oldMan.transform.position - transform.position;
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5F, 0.5F, 0));
			if(Physics.Raycast(ray, out hit, 20))
			{
				if(hit.transform.tag == "OldMan")
				{
					oldManSeen = true;
				}
				else
				{
					oldManSeen = false;
				}
				Debug.DrawLine(transform.position, hit.point, Color.blue);
				//print ("Looking at " + hit.transform.name);
			}
			if ((Vector3.Angle(rayDirection, transform.forward)) <= oldManViewAngle * 0.5f)
			{
				peripheral = true;
			}
			else
			{
				peripheral = false;
			}
			if((Vector3.Angle(rayDirection, transform.forward)) > oldManViewAngle * 0.5f)
			{
				oldManSeen = false;
			}
			if((Camera.main.transform.eulerAngles.x <= 330f && Camera.main.transform.eulerAngles.x > 60f))
			{
				peripheral = false;
			}
		}
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
}

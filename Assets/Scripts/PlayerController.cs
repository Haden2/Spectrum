using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameObject elevator;
	GameObject oldMan;
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
	public Vector3 downPosition;
	public Vector3 upPosition;
	float distance;
	float power;
	float fieldOfViewDegrees = 100;
	float oldManViewAngle = 50;

	void Start()
	{
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
		distance = 30f;
		power = 500;
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
		//if(Input.GetMouseButton(0))
		{
			//Ray rayOrigin = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hitInfo;
			Vector3 rayDirection = oldMan.transform.position - transform.position;
			if ((Vector3.Angle(rayDirection, transform.forward)) <= oldManViewAngle * 0.5f)
			{
				if(Physics.Raycast(transform.position, rayDirection, out hitInfo, 20))
				{
					Debug.DrawLine(transform.position, hitInfo.point);
					if(hitInfo.transform.tag == "OldMan")
					{
						oldManSeen = true;
					}
					else
					{
						oldManSeen = false;
					}
				//hitInfo.rigidbody.AddForceAtPosition(transform.position *power, hitInfo.point);
				}
			}
			if((Vector3.Angle(rayDirection, transform.forward)) > oldManViewAngle * 0.5f)
			{
				oldManSeen = false;
			}
			if((Camera.main.transform.eulerAngles.x <= 300f && Camera.main.transform.eulerAngles.x > 60f))
			{
				oldManSeen = false;
			}
			print (rayDirection);
			/*if(Physics.Raycast (rayOrigin, Vector3.forward, out hitInfo))
			{
				Debug.Log("casting ray");
				Debug.DrawLine(rayOrigin.direction, hitInfo.point);
				if(hitInfo.rigidbody != null)
				{
					hitInfo.rigidbody.AddForceAtPosition(rayOrigin.direction * power, hitInfo.point);
				}
			}*/
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

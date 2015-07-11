using UnityEngine;
using System.Collections;

public class Environmental : MonoBehaviour {

	//Elevator
	public Inventory inventory;
	public GameObject elevator;
	public GameObject bigDoor;
	public GameObject littleDoor;
	public bool elevatorDoor;
	public bool inElevator;
	public bool doorIsClosed;
	public bool canActive;
	public bool isDown;
	public bool isUp;
	public bool isMoving;
	public Vector3 downPosition;
	public Vector3 upPosition;

	//Door
	public GameObject key;
	public GameObject player;
	public GameObject doorR;
	public GameObject doorL;
	public GameObject pivotR;		
	public GameObject pivotL;
	/*public Transform doorR;
	public Transform doorL;
	public Transform pivotR;
	public Transform pivotL;*/
	Vector3 AngleR;
	Vector3 closingAngleR;
	public bool open;
	public bool closed;
	public bool closing;
	public bool rotating;
	public bool nothing;
	public bool haveKey;
	public bool awaitingKey;




	// Use this for initialization
	void Start () 
	{
		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory> ();
		elevator = GameObject.Find("Elevator");
		bigDoor = GameObject.Find ("Big Door");
		littleDoor = GameObject.Find ("Little Door");
		downPosition = new Vector3(13, 1, -5);
		upPosition = new Vector3 (13, 15, -5);
		isDown = true;
		isUp = false;
		isMoving = false;


		doorR = GameObject.Find ("DoorR");
		doorL = GameObject.Find ("DoorL");
		pivotR = GameObject.Find ("PivotR");
		pivotL = GameObject.Find ("PivotL");
		closed = true;
		closing = false;
		open = false;
		haveKey = false;
		rotating = false; 
		nothing = true;
		key = GameObject.FindGameObjectWithTag ("Key");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void Update () 
	{
		if(awaitingKey)
		{
			if(inventory.activeKey)
			{
				StartCoroutine(WantToOpenDoor());
			}
		}
		if(elevatorDoor && inventory.activeElevatorKey && canActive == true && Input.GetKeyDown("e"))
		{
			littleDoor.GetComponent<Animation>().Play("SmallElevatorDoor");
			bigDoor.GetComponent<Animation>().Play("ElevatorDoor");
		}
		if(isDown == true && isUp == false && isMoving == false && canActive == true && inElevator && Input.GetKeyDown ("1"))
		{
			StartCoroutine(MoveUp (elevator.transform, downPosition, upPosition, 5));
		}
		if(isDown == false && isUp == true && isMoving == false && canActive == true && inElevator && Input.GetKeyDown ("1"))
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


		if(closed == true && haveKey == true && nothing == false && Input.GetKeyDown ("e")/*Event.current.button == 0 && Event.current.type == EventType.mouseUp*/)
		{
			StartCoroutine(DoorOpens());
		}
		
		if(rotating)
		{
			Vector3 AngleR = new Vector3(0,359,0);
			Vector3 AngleL = new Vector3(0,0,0);
			Vector3 newAngleL = new Vector3(0,-90,0);
			//RIGHT DOOR
			if(Vector3.Distance(doorR.transform.eulerAngles, AngleR) > .01f)
			{
				doorR.transform.RotateAround(pivotR.transform.position, Vector3.Lerp (doorR.transform.rotation.eulerAngles, AngleR, Time.deltaTime), .5f);
			}
			else
			{
				doorR.transform.eulerAngles = AngleR;
				rotating = false;
			}
			//LEFT DOOR
			if(Vector3.Distance(doorL.transform.eulerAngles, AngleL) > .01f)
			{
				doorL.transform.RotateAround(pivotL.transform.position, newAngleL, .5f);
			}
			else
			{
				doorL.transform.eulerAngles = AngleL;
				rotating = false;
			}
		}
		//RIGHT DOOR
		if(open == false && closing == true)
		{
			Vector3 closingAngleR = new Vector3(0,270,0);
			Vector3 reverseAngleR = new Vector3(0,-90,0);
			
			if(Vector3.Distance(doorR.transform.eulerAngles, closingAngleR) > .01f)
			{
				doorR.transform.RotateAround(pivotR.transform.position, reverseAngleR, .5f);
			}
			else
			{
				doorR.transform.eulerAngles = closingAngleR;
				rotating = false;
			}
		}
		//LEFT DOOR
		if(open == false && closing == true)
		{
			Vector3 closingAngleL = new Vector3(0,90,0);
			
			if(Vector3.Distance(doorL.transform.eulerAngles, closingAngleL) > .01f)
			{
				doorL.transform.RotateAround(pivotL.transform.position, closingAngleL, .5f);
			}
			else
			{
				doorL.transform.eulerAngles = closingAngleL;
				rotating = false;
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



	public IEnumerator WantToOpenDoor()
	{
		print ("Open Door?");
		nothing = false;
		haveKey = true;
		yield return new WaitForSeconds (0);
	}
	
	public IEnumerator DoorOpens ()
	{
		rotating = true;
		open = true;
		yield return new WaitForSeconds (5);
		rotating = false;
		open = false;
		closing = true;
	}
	
	public IEnumerator DoNothing()
	{
		print ("Nope");
		nothing = true;
		haveKey = false;
		awaitingKey = false;
		yield return new WaitForSeconds (0);
	}
	
	public IEnumerator CantOpenDoor()
	{
		haveKey = false;
		print ("Need Key First");
		yield return new WaitForSeconds (0);
	}

}

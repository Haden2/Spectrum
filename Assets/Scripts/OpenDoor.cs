using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	/*public GameObject key;
	GameObject player;
	public Transform doorR;
	public Transform doorL;
	public Transform pivotR;
	public Transform pivotL;
	Vector3 AngleR;
	Vector3 closingAngleR;
	bool open;
	bool closed;
	bool closing;
	public bool haveKey;
	bool rotating;
	bool nothing;

	void Start () 
	{
		//StartCoroutine(DoorOpens());
		closed = true;
		closing = false;
		open = false;
		haveKey = false;
		rotating = false; 
		nothing = true;
		key = GameObject.FindGameObjectWithTag ("Key");
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider other)
	{

	}

	void OnTriggerExit (Collider other)
	{

	}

	IEnumerator WantToOpenDoor()
	{
		print ("Open Door?");
		nothing = false;
		haveKey = true;
		yield return new WaitForSeconds (0);
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

	IEnumerator DoNothing()
	{
		print ("Nope");
		nothing = true;
		haveKey = false;
		yield return new WaitForSeconds (0);
	}

	IEnumerator CantOpenDoor()
	{
		haveKey = false;
		print ("Need Key First");
		yield return new WaitForSeconds (0);
	}

	void Update () 
	{
		if(closed == true && haveKey == true && nothing == false && Input.GetKeyDown ("e")
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
				doorR.transform.RotateAround(pivotR.position, Vector3.Lerp (doorR.transform.rotation.eulerAngles, AngleR, Time.deltaTime), .5f);
			}
			else
			{
				doorR.transform.eulerAngles = AngleR;
				rotating = false;
			}
		//LEFT DOOR
			if(Vector3.Distance(doorL.transform.eulerAngles, AngleL) > .01f)
			{
				doorL.transform.RotateAround(pivotL.position, newAngleL, .5f);
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
				doorR.transform.RotateAround(pivotR.position, reverseAngleR, .5f);
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
				doorL.transform.RotateAround(pivotL.position, closingAngleL, .5f);
			}
			else
			{
				doorL.transform.eulerAngles = closingAngleL;
				rotating = false;
			}
		}
	}*/
}

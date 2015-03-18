using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	GameObject key;
	public Transform door;
	public Transform pivot;
	public Vector3 Angle;
	Vector3 closingAngle;
	bool open;
	bool closed;
	bool closing;
	bool haveKey;
	bool rotating;

	void Start () 
	{
		closed = true;
		closing = false;
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

	IEnumerator WantToOpenDoor()
	{
		print ("Open Door?");
		haveKey = true;
		yield return new WaitForSeconds (0);
	}

	IEnumerator DoorOpens ()
	{
		rotating = true;
		open = true;
		yield return new WaitForSeconds (5);
		open = false;
		closing = true;
	}

	//IEnumerator DoorCloses()
	//{

	//}

	void Update () 
	{
		if(closed == true && haveKey == true && Input.GetKeyDown ("e")/*Event.current.button == 0 && Event.current.type == EventType.mouseUp*/)
		{
			StartCoroutine(DoorOpens());
		}
		if(closed == true && haveKey == false && Input.GetKeyDown ("e"))
		{
			//print ("Need Key First");
		}
		if(rotating)
		{
			Vector3 Angle = new Vector3(0,359,0);
			if(Vector3.Distance(transform.eulerAngles, Angle) > .01f)
			{
				transform.RotateAround(pivot.position, Vector3.Lerp (transform.rotation.eulerAngles, Angle, Time.deltaTime), .5f);
			}
			else
			{
				transform.eulerAngles = Angle;
				//rotating = false;
			}
		}
		if(open == false && closing == true)
			{
				print ("closing door");
				Vector3 closingAngle = new Vector3(0,270,0);
				if(Vector3.Distance(transform.eulerAngles, Angle) > .01f)
				{
					transform.RotateAround(pivot.position, Vector3.Lerp (transform.rotation.eulerAngles, closingAngle, Time.deltaTime), .5f);
				}
				else
				{
					transform.eulerAngles = closingAngle;
					//rotating = false;
				}
			}
		}
}

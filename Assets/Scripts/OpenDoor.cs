using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	GameObject key;
	public Transform door;
	public Transform pivot;
	public Vector3 Angle;
	bool open;
	bool closed;
	bool haveKey;
	bool rotating;

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

	IEnumerator WantToOpenDoor()
	{
		print ("Open Door?");
		haveKey = true;
		yield return new WaitForSeconds (0);
	}

	void Update () 
	{
		if(closed == true && haveKey == true && Input.GetKeyDown ("e")/*Event.current.button == 0 && Event.current.type == EventType.mouseUp*/)
		{
			print ("Opening Door");
			rotating = true;
			open = true;
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
				rotating = false;
			}

		}
	}
	void OpenThisDoor()
	{
	
	}

}

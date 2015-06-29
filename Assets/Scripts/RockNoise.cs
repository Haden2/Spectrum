using UnityEngine;
using System.Collections;

public class RockNoise : MonoBehaviour {

	public string hitobject;
	public bool isgrounded;
	public bool standingOn;
	public CollectItem collect;
	public Inventory inventory;
	public float realTime;
	public float thrownTime;

	void Start () 
	{
		collect = GameObject.Find ("First Person Controller").GetComponent<CollectItem> ();
		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory> ();
		thrownTime = Time.realtimeSinceStartup;
	}
	
	void Update () 
	{
		realTime = Time.realtimeSinceStartup;
		if(standingOn && Input.GetKeyDown("e") && collect.holdStill == false)
		{
			inventory.AddItem(0);
			collect.pressE.color = collect.blank;
			standingOn = false;
			collect.onItem = false;
			collect.rockIsGot = true;
			Destroy(gameObject);		
		}
	}

	void OnCollisionEnter(UnityEngine.Collision hit)
	{
		hitobject = hit.gameObject.tag;
		//if(hitobject == "Floor")
		{
			isgrounded = true;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(realTime - thrownTime > 1)
		{
			if(other.gameObject.name == "First Person Controller")
			{
				standingOn = true;
				collect.pressE.color = collect.pickupText;
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		collect.pressE.color = collect.blank;
	}
}

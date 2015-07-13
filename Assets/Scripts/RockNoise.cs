using UnityEngine;
using System.Collections;

public class RockNoise : MonoBehaviour {

	public string hitobject;
	public bool isgrounded;
	public bool standingOn;

	public float realTime;
	public float thrownTime;

	public CollectItem collect;
	public Inventory inventory;
	public EchoSpherez echoSpherez;

	void Start () 
	{
		thrownTime = Time.realtimeSinceStartup;
		collect = GameObject.Find ("First Person Controller").GetComponent<CollectItem> ();
		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory> ();
		echoSpherez = GameObject.Find ("First Person Controller").GetComponent<EchoSpherez> ();
		echoSpherez.isGrounded = true;
	}
	
	void Update () 
	{
		realTime = Time.realtimeSinceStartup;
		if(standingOn && Input.GetKeyDown("e") && collect.holdStill == false)
		{
			echoSpherez.isGrounded = false;
			isgrounded = false;
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
		if(hitobject == "Environment")
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
		standingOn = false;
	}
}

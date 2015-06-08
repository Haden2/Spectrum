using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public Inventory inventory;
	NavMeshAgent nav;
	Vector3 worldDeltaPosition;
	GameObject gloves;
	GameObject Player;
	float deathSequence;
	bool jump;
	
	// Use this for initialization
	void Start () 
	{
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		gloves = GameObject.FindGameObjectWithTag ("Gloves");
		Player = GameObject.FindGameObjectWithTag ("Player");
		deathSequence = 3;
		nav = GetComponent<NavMeshAgent> ();
		nav.speed = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(jump)
		{
			//transform.position = (worldDeltaPosition);
		}
		Vector3 endPivotDir = Player.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, endPivotDir, 1,10);
		//transform.rotation = Quaternion.LookRotation(newDir);
		worldDeltaPosition = Player.transform.position - transform.position;
		print (worldDeltaPosition);
		nav.SetDestination (Player.transform.position);
		if(worldDeltaPosition.z == 10)
		{
			jump = true;
			print ("Further away");
			StartCoroutine (JumpForward());
		}
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == gloves)
		{
			print ("collided with gloves");
			StartCoroutine (DeathSequence());
		}
	}

	IEnumerator JumpForward()
	{
		//nav.speed = 1;
		//nav.SetDestination (worldDeltaPosition + new Vector3(0,0,5));
		transform.position = (worldDeltaPosition + new Vector3(0,0,1));
		yield return null;
	}

	IEnumerator DeathSequence()
	{
		inventory.activeGloves = false;
		foreach(GameObject _obj in inventory.holdingGloves)
		{
			_obj.SetActive(false);
		}
		inventory.glovesSwap = false;
		yield return new WaitForSeconds (deathSequence);
		gameObject.SetActive (false);
	}
}

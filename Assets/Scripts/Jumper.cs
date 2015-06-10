using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public Inventory inventory;
	NavMeshAgent nav;
	Vector3 worldDeltaPosition;
	GameObject gloves;
	GameObject Player;
	float deathSequence;
	public bool jumpForward;
	public bool jumpRight;
	public bool jumpBack;
	public bool jumpLeft;
	
	// Use this for initialization
	void Start () 
	{
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		gloves = GameObject.FindGameObjectWithTag ("Gloves");
		Player = GameObject.FindGameObjectWithTag ("Player");
		deathSequence = 3;
		nav = GetComponent<NavMeshAgent> ();
		nav.speed = 0;
		StartCoroutine(JumpForward());
		StartCoroutine(JumpBack());
		StartCoroutine(JumpRight());
		StartCoroutine(JumpLeft());
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 endPivotDir = Player.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, endPivotDir, .1f,1);
		transform.rotation = Quaternion.LookRotation(newDir);
		worldDeltaPosition = Player.transform.position - transform.position;
		//print (worldDeltaPosition);
		nav.SetDestination (Player.transform.position);
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
		print ("Courotine");
		if(worldDeltaPosition.z >= 2)
		{
			print ("In If Loop");
			//jumpForward = true;
			transform.position = (transform.position + new Vector3(0,0,2));
		}
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (JumpForward());
	}
	IEnumerator JumpRight()
	{
		if(worldDeltaPosition.x >= 2)
		{
			transform.position = (transform.position + new Vector3(2,0,0));
		}
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (JumpRight());
	}
	IEnumerator JumpBack()
	{
		if(worldDeltaPosition.z < -2)
		{
			transform.position = (transform.position + new Vector3(0,0,-2));
		}	
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (JumpBack());
	}
	IEnumerator JumpLeft()
	{
		if(worldDeltaPosition.x < -2)
		{
			transform.position = (transform.position + new Vector3(-2,0,0));
		}
		yield return new WaitForSeconds (1.5f);
		StartCoroutine (JumpLeft());
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

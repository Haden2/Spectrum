using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public Inventory inventory;
	NavMeshAgent nav;
	Vector3 worldDeltaPosition;
	GameObject gloves;
	GameObject Player;
	float deathSequence;
	float jumpForward;
	float jumpRight;
	float jumpBack;
	float jumpLeft;
	float randomForward;
	float randomRight;
	float randomBack;
	float randomLeft;
	float lookingAngle;

	
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
		lookingAngle = transform.eulerAngles.y;
		print (lookingAngle);
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
		if(lookingAngle >= 225 && lookingAngle <= 315)
		{
			jumpForward = Random.Range (.5f, 2);
			randomForward = Random.Range (1.5f, 3);
			if(worldDeltaPosition.z >= 2)
			{
				transform.position = (transform.position + new Vector3(0,0,jumpForward));
			}
		}
		//if(lookingAngle >= 45 && lookingAngle <= 135f)

		jumpForward = Random.Range (4, 6);
		randomForward = Random.Range (.5f, 3);

		yield return new WaitForSeconds (randomForward);
		StartCoroutine (JumpForward());
	}
	IEnumerator JumpRight()
	{
		randomRight = Random.Range (1.5f, 3);
		jumpRight = Random.Range (.5f, 2);

		if(worldDeltaPosition.x >= 2)
		{
			transform.position = (transform.position + new Vector3(jumpRight,0,0));
		}
		yield return new WaitForSeconds (randomRight);
		StartCoroutine (JumpRight());
	}
	IEnumerator JumpBack()
	{
		randomBack = Random.Range (.5f, 3);
		jumpBack = Random.Range (4, 6);
		if(worldDeltaPosition.z < -2)
		{
			print ("Jumped Backward");

			transform.position = (transform.position + new Vector3(0,0,-jumpBack));
		}	
		yield return new WaitForSeconds (randomBack);
		StartCoroutine (JumpBack());
	}
	IEnumerator JumpLeft()
	{
		if(lookingAngle >= 225 && lookingAngle <= 315)
		{
			jumpLeft = Random.Range (4, 6);
			randomLeft = Random.Range (.5f, 3);
			if(worldDeltaPosition.x < -2)
			{
				transform.position = (transform.position + new Vector3(-jumpLeft,0,0));
			}
		}
		randomLeft = Random.Range (1.5f, 3);
		jumpLeft = Random.Range (.5f, 2);

		yield return new WaitForSeconds (randomLeft);
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

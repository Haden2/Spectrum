using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public Inventory inventory;
	CharacterMotorC playerMovement;
	NavMeshAgent nav;
	Vector3 worldDeltaPosition;
	GameObject gloves;
	GameObject Player;
	float deathSequence;
	float jumpZ;
	float jumpX;
	float randomZ;
	float randomX;
	float lookingAngle;

	
	// Use this for initialization
	void Start () 
	{
		playerMovement = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterMotorC> ();
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		gloves = GameObject.FindGameObjectWithTag ("Gloves");
		Player = GameObject.FindGameObjectWithTag ("Player");
		deathSequence = 3;
		nav = GetComponent<NavMeshAgent> ();
		nav.speed = 0;
		StartCoroutine(JumpForward());
	}
	
	// Update is called once per frame
	void Update () 
	{
		lookingAngle = transform.eulerAngles.y;
		//print (lookingAngle);
		Vector3 endPivotDir = Player.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, endPivotDir, .1f,1);
		transform.rotation = Quaternion.LookRotation(newDir);
		worldDeltaPosition = Player.transform.position - transform.position;
		nav.SetDestination (Player.transform.position);
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == gloves)
		{
			print ("collided with gloves");
			StartCoroutine (DeathSequence());
		}
		if(other.gameObject == Player)
		{
			StartCoroutine(TeleportPlayer());
		}
	}

	IEnumerator JumpForward()
	{
		//print (worldDeltaPosition);

		//In the X plane going left or right
		if(lookingAngle > 225 && lookingAngle < 315 || lookingAngle > 45 && lookingAngle < 135)
		{
			jumpX = Random.Range (3,5);
			jumpZ = Random.Range (2, 4);
			if(worldDeltaPosition.z >= 2)
			{
				transform.position = (transform.position + new Vector3(0,0,jumpZ));
			}
			if(worldDeltaPosition.z <= -2)
			{
				transform.position = (transform.position + new Vector3(0,0,-jumpZ));
			}
			if(worldDeltaPosition.x >= 2)
			{
				transform.position = (transform.position + new Vector3(jumpX,0,0));
			}
			if(worldDeltaPosition.x <= -2)
			{
				transform.position = (transform.position + new Vector3(-jumpX,0,0));
			}
		}
		//In the Z plane going forwards or backwards
		if(((lookingAngle < 45 && lookingAngle > 0) || (lookingAngle > 315 && lookingAngle < 360)) || (lookingAngle > 135 && lookingAngle < 225))
		{
			jumpX = Random.Range (2,4);
			jumpZ = Random.Range (3, 5);
			if(worldDeltaPosition.z >= 2)
			{
				transform.position = (transform.position + new Vector3(0,0,jumpZ));
			}
			if(worldDeltaPosition.z <= -2)
			{
				transform.position = (transform.position + new Vector3(0,0,-jumpZ));
			}
			if(worldDeltaPosition.x >= 2)
			{
				transform.position = (transform.position + new Vector3(jumpX,0,0));
			}
			if(worldDeltaPosition.x <= -2)
			{
				transform.position = (transform.position + new Vector3(-jumpX,0,0));
			}
		}
		if(worldDeltaPosition.z >= 15 || worldDeltaPosition.z <= -15 || worldDeltaPosition.x >= 15 || worldDeltaPosition.x <= -15)
		{
			print ("Fast Mode");
			randomZ = Random.Range (.1f,.5f);
		}
		if((worldDeltaPosition.z < 15 && worldDeltaPosition.z > -15) && (worldDeltaPosition.x < 15 && worldDeltaPosition.x > -15))
		{
			randomZ = Random.Range (1f, 3);
		}
		yield return new WaitForSeconds (randomZ);
		StartCoroutine (JumpForward());
	}
	IEnumerator TeleportPlayer()
	{
		playerMovement.canControl = false;
		//playerMovement.inputMoveDirection = Vector3.zero;
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

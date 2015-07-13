using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public GameObject gloves;
	public GameObject Player;

	public float deathSequence;
	public float jumpZ;
	public float jumpX;
	public float randomZ;
	public float randomX;
	public float lookingAngle;

	public Vector3 worldDeltaPosition;
	
	public Inventory inventory;
	public CharacterMotorC playerMovement;
	public NavMeshAgent nav;

	
	// Use this for initialization
	void Start () 
	{
		gloves = GameObject.Find ("Gloves");
		Player = GameObject.Find ("First Person Controller");

		deathSequence = 3;

		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory>();
		playerMovement = GameObject.Find ("First Person Controller").GetComponent<CharacterMotorC> ();
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
			//print ("collided with gloves");
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
			//print ("Fast Mode");
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
		if(inventory.activeGloves)
		{
			StartCoroutine (DeathSequence());
		}
		playerMovement.canControl = false;
		//playerMovement.inputMoveDirection = Vector3.zero;
		yield return null;
	}

	IEnumerator DeathSequence()
	{
		inventory.activeGloves = false;
		inventory.glovesSwap = false;
		yield return new WaitForSeconds (deathSequence);
		foreach(GameObject _obj in inventory.holdingGloves)
		{
			_obj.SetActive(false);
		}
		playerMovement.canControl = true;
		gameObject.SetActive (false);
	}
}

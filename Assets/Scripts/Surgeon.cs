using UnityEngine;
using System.Collections;

public class Surgeon : MonoBehaviour {

	public GameObject Torso;
	public GameObject LeftArm;
	public GameObject RightLeg;
	public GameObject LeftLeg;
	public GameObject AdditionalPart;
	public GameObject Player;
	public GameObject poisonhead;

	public bool torso;
	public bool leftArm;
	public bool rightLeg;
	public bool leftLeg;
	public bool additionalPart;
	public bool kill;

	public float deathSequence;
	public float surgery;

	public Inventory inventory;
	public NavMeshAgent bodyBuilding;

	
	// Use this for initialization
	void Start () 
	{
		Torso = GameObject.Find ("Torso");
		LeftArm = GameObject.Find ("LeftArm");
		RightLeg = GameObject.Find ("RightLeg");
		LeftLeg = GameObject.Find ("LeftLeg");
		AdditionalPart = GameObject.Find ("AdditionalParts");
		Player = GameObject.Find ("First Person Controller");
		poisonhead = GameObject.Find ("HoldingPoisonHead");

		torso = true;
		deathSequence = 3;
		surgery = 2; //12

		bodyBuilding = GetComponent<NavMeshAgent>();
		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(torso)
		{
			bodyBuilding.speed = .5f; //.5
			bodyBuilding.SetDestination(Torso.transform.position);
		}
		if(leftArm)
		{
			bodyBuilding.speed = 1; //1
			bodyBuilding.SetDestination(LeftArm.transform.position);
		}
		if(rightLeg)
		{
			bodyBuilding.speed = 2;
			bodyBuilding.SetDestination(RightLeg.transform.position);
		}
		if(leftLeg)
		{
			bodyBuilding.speed = 3;
			bodyBuilding.SetDestination(LeftLeg.transform.position);
		}
		if(additionalPart)
		{
			bodyBuilding.speed = 4;
			bodyBuilding.SetDestination(AdditionalPart.transform.position);
		}
		if(kill)
		{
			bodyBuilding.speed = 5;
			bodyBuilding.SetDestination(Player.transform.position);
		}
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == poisonhead)
		{
			//print ("collided with poisonhead");
			StartCoroutine (DeathSequence());
		}

		if(other.gameObject == Torso)
		{
			StartCoroutine (LeftArmSearch());
		}
		if(other.gameObject == LeftArm)
		{
			StartCoroutine (RightLegSearch());
		}
		if(other.gameObject == RightLeg)
		{
			StartCoroutine (LeftLegSearch());
		}
		if(other.gameObject == LeftLeg)
		{
			StartCoroutine (AdditionalSearch());
		}
		if(other.gameObject == AdditionalPart)
		{
			StartCoroutine (Kill());
		}
		if(other.gameObject == Player && kill)
		{
			kill = false;
			bodyBuilding.speed = 0;
		}
	}
	
	IEnumerator DeathSequence()
	{
		inventory.activePoisonHead = false;
		inventory.holdingPoisonHead.SetActive(false);
		inventory.poisonheadSwap = false;
		yield return new WaitForSeconds (deathSequence);
		gameObject.SetActive (false);
	}
	IEnumerator LeftArmSearch()
	{
		torso = false;
		yield return new WaitForSeconds (surgery);
		Torso.gameObject.SetActive (false);
		leftArm = true;
	}
	IEnumerator RightLegSearch()
	{
		leftArm = false;
		yield return new WaitForSeconds (surgery);
		LeftArm.gameObject.SetActive (false);
		rightLeg = true;
	}
	IEnumerator LeftLegSearch()
	{
		rightLeg = false;
		yield return new WaitForSeconds (surgery);
		RightLeg.gameObject.SetActive (false);
		leftLeg = true;
	}
	IEnumerator AdditionalSearch()
	{
		leftLeg = false;
		yield return new WaitForSeconds (surgery);
		LeftLeg.gameObject.SetActive (false);
		additionalPart = true;
	}
	IEnumerator Kill()
	{
		additionalPart = false;
		yield return new WaitForSeconds (surgery);
		AdditionalPart.gameObject.SetActive (false);
		kill = true;
	}
}

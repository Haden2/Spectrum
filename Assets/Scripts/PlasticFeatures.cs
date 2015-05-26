using UnityEngine;
using System.Collections;

public class PlasticFeatures : MonoBehaviour {

	//GameObject plastic;
	public Inventory inventory;
	GameObject anatomy;
	float deathSequence;


	// Use this for initialization
	void Start () 
	{
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		anatomy = GameObject.FindGameObjectWithTag ("Anatomy");
		deathSequence = 3;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == anatomy)
		{
			print ("collided with anatomy");
			StartCoroutine (DeathSequence());
		}
	}

	IEnumerator DeathSequence()
	{
		inventory.activeAnatomy = false;
		inventory.holdingAnatomy.SetActive(false);
		inventory.anatomySwap = false;
		yield return new WaitForSeconds (deathSequence);
		gameObject.SetActive (false);
	}
}

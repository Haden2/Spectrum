using UnityEngine;
using System.Collections;

public class PlasticFeatures : MonoBehaviour {

	//GameObject plastic;
	public GameObject player;
	public GameObject anatomy;
	public GameObject eye;

	public float deathSequence;
	
	public Inventory inventory;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("First Person Controller");
		anatomy = GameObject.Find ("HoldingAnatomy");
		eye = GameObject.Find ("Eye");

		deathSequence = 3;

		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 endPivotDir = player.transform.position - eye.transform.position;
		//float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (eye.transform.forward, endPivotDir, 1,10);
		Debug.DrawRay(eye.transform.position, newDir, Color.red);
		eye.transform.rotation = Quaternion.LookRotation(newDir);
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == anatomy)
		{
			//print ("collided with anatomy");
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

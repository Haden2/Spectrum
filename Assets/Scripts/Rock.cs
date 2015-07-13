using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Rigidbody rockProjectile;
	public Transform rockLocation;
	public int throwingPower;
	public Inventory inventory;
	public CollectItem collect;


	void Start () 
	{
		rockProjectile = rockProjectile.GetComponent<Rigidbody> ();
		throwingPower = 20;
		inventory = GameObject.Find ("First Person Controller").GetComponent<Inventory>();
		collect = GameObject.Find ("First Person Controller").GetComponent<CollectItem> ();
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Fire1") && inventory.showInventory == false && inventory.firstThrow)
		{
			//print ("throwing");
			Rigidbody clone = Instantiate(rockProjectile, rockLocation.transform.position, rockLocation.transform.rotation) as Rigidbody;
			clone.velocity = rockLocation.transform.TransformDirection (Vector3.forward * throwingPower);
			inventory.activeRock = false;
			collect.rockIsGot = false;		
		}
	}
}

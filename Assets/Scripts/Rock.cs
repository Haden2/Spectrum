using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Rigidbody rockProjectile;
	public CollectItem collect;
	public Transform rockLocation;
	public int throwingPower;
	public Inventory inventory;

	void Start () 
	{
		rockProjectile = rockProjectile.GetComponent<Rigidbody> ();
		throwingPower = 20;
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		collect = GameObject.FindGameObjectWithTag ("Player").GetComponent<CollectItem> ();
	}
	
	void Update () 
	{
		if(Input.GetButtonDown("Fire1") && inventory.showInventory == false && inventory.firstThrow)
		{
			print ("throwing");
			Rigidbody clone = Instantiate(rockProjectile, rockLocation.transform.position, rockLocation.transform.rotation) as Rigidbody;
			clone.velocity = rockLocation.transform.TransformDirection (Vector3.forward * throwingPower);
			inventory.activeRock = false;
			collect.rockIsGot = false;
			
		}
	}
}

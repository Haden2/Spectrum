using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Rigidbody projectile; //Prefab bullet
	public Rigidbody rockProjectile;
	public Transform bulletLocation;
	public Transform rockLocation;
	public CollectItem collect;
	public int shootingPower;
	public int throwingPower;
	public Inventory inventory;

	// Use this for initialization
	void Start () 
	{
		projectile = projectile.GetComponent<Rigidbody> ();
		rockProjectile = rockProjectile.GetComponent<Rigidbody> ();
		shootingPower = 150;
		throwingPower = 20;
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		collect = GameObject.FindGameObjectWithTag ("Player").GetComponent<CollectItem> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown ("Fire1") && inventory.showInventory == false && inventory.firstShot && inventory.activeGun)
		{
			print ("shooting");
			Rigidbody clone = Instantiate(projectile, bulletLocation.transform.position, bulletLocation.transform.rotation) as Rigidbody;
			clone.velocity = bulletLocation.transform.TransformDirection (Vector3.up * shootingPower);
		}
		if(Input.GetButtonDown("Fire1") && inventory.showInventory == false && inventory.firstThrow)
		{
			//rockProjectile.maxAngularVelocity = 10;
			print ("throwing");
			Rigidbody clone = Instantiate(rockProjectile, rockLocation.transform.position, rockLocation.transform.rotation) as Rigidbody;
			clone.velocity = rockLocation.transform.TransformDirection (Vector3.forward * throwingPower);
			inventory.activeRock = false;
			collect.rockIsGot = false;

		}
	}
}

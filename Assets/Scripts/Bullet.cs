using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Rigidbody projectile; //Prefab bullet
	public Transform bulletLocation;
	public GameObject bulletHole;
	public CollectItem collect;
	public Inventory inventory;
	public int shootingPower;
	public bool oldManShot;
	
	void Start () 
	{
		projectile = projectile.GetComponent<Rigidbody> ();
		shootingPower = 150;
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		collect = GameObject.FindGameObjectWithTag ("Player").GetComponent<CollectItem> ();
	}

	void Update () 
	{
		if(Input.GetButtonDown ("Fire1") && inventory.showInventory == false && inventory.firstShot && inventory.activeGun)
		{
			Rigidbody clone = Instantiate(projectile, bulletLocation.transform.position, bulletLocation.transform.rotation) as Rigidbody;
			clone.velocity = bulletLocation.transform.TransformDirection (Vector3.up * shootingPower);

			RaycastHit hit;
			Ray ray = new Ray(transform.position, transform.up);
			if(Physics.Raycast(ray, out hit, 100f))
			{
				GameObject targetGameObject = hit.collider.gameObject; // What's the GameObject?
				print (targetGameObject);
				if(hit.transform.tag == "OldMan")
				{
					oldManShot = true;
				}
				if(hit.transform.tag == "Environment")
				{
					Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
				}
			}
			Debug.DrawLine(transform.position, hit.point, Color.green);
			/*if(Physics.Raycast (rayOrigin, Vector3.forward, out hitInfo))
			{
				if(hitInfo.rigidbody != null)
				{
					hitInfo.rigidbody.AddForceAtPosition(rayOrigin.direction * power, hitInfo.point);
				}
			}*/
		}
	}
}

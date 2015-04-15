using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Rigidbody projectile; //Prefab bullet
	public int shootingPower;
	// Use this for initialization
	void Start () 
	{
		projectile = projectile.GetComponent<Rigidbody> ();
		shootingPower = 150;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown ("Fire1"))
		{
			Rigidbody clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
			clone.velocity = transform.TransformDirection (Vector3.up * shootingPower);
		}
	}
}

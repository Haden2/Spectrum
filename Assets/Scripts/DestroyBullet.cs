using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		//Destroy (gameObject, 3.0f); Bounces off game object.
	}

	void OnCollisionEnter()
	{
		Destroy (gameObject); // destroys on contact with collider.
	}
}

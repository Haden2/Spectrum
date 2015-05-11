using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Destroy (gameObject, 3.0f);
	}
}

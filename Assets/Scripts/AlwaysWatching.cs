using UnityEngine;
using System.Collections;

public class AlwaysWatching : MonoBehaviour {

	public Transform player;
	public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 endPivotDir = player.position - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, endPivotDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}

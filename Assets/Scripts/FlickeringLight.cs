using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

	public Light lightBulb;
	public Light smallFlicker;

	// Use this for initialization
	void Start () 
	{
		lightBulb = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		lightBulb.intensity = Random.Range (0f, 2f);
		lightBulb.range = Random.Range (0f, 30f);

		smallFlicker.intensity = Random.Range (3f, 4f);
		smallFlicker.range = Random.Range (14f, 16f);
	}
}

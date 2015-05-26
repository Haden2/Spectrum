using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

	public Inventory inventory;
	public Light lightBulb;
	public Light smallFlicker;
	public float randomNumber;
	public float randomNumber1;
	public float randomNumber2;
	public float randomNumber3;

	// Use this for initialization
	void Start () 
	{
		lightBulb = GetComponent<Light> ();
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		smallFlicker = GameObject.Find ("SmallFlicker").GetComponent <Light>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(inventory.showInventory == false)
		{
		randomNumber = Random.Range (0f, 2f);
		randomNumber1 = Random.Range (0f, 30f);
		randomNumber2 = Random.Range (3f, 4f);
		randomNumber3 = Random.Range (14f, 16f);

		lightBulb.intensity = Random.Range (0f, 2f);
		lightBulb.range = Random.Range (0f, 30f);
		smallFlicker.intensity = Random.Range (3f, 4f);
		smallFlicker.range = Random.Range (14f, 16f);
		}

		if(inventory.showInventory == true)
		{
			lightBulb.intensity = randomNumber;
			lightBulb.range = randomNumber1;
			smallFlicker.intensity = randomNumber2;
			smallFlicker.range = randomNumber3;
		}
	}
	public void RandomNumber()
	{

	}
}

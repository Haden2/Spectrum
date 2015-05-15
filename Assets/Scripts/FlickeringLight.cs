using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour {

	public Inventory inventory;
	public Light lightBulb;
	public Light smallFlicker;
	public int randomNumber;

	// Use this for initialization
	void Start () 
	{
		lightBulb = GetComponent<Light> ();
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<Inventory>();
		smallFlicker = GameObject.Find ("SmallFlicker").GetComponent <Light>();
		randomNumber = Random.Range (1, 6);
		print ("Random Number is " + randomNumber);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(inventory.showInventory == false)
		{
		lightBulb.intensity = Random.Range (0f, 2f);
		lightBulb.range = Random.Range (0f, 30f);
			randomNumber = Random.Range (1,6);
			print ("Random Number is " + randomNumber);
		smallFlicker.intensity = Random.Range (3f, 4f);
		smallFlicker.range = Random.Range (14f, 16f);

		}
		if(inventory.showInventory == true && randomNumber == 1)
		{
			randomNumber = Random.Range (1,6);
			print("Second Random Number "+ randomNumber);
			lightBulb.intensity = 1f;
			lightBulb.range = 15f;
			smallFlicker.intensity = 3.5f;
			smallFlicker.range = 15f;
		}
	}
	public void RandomNumber()
	{

	}
}

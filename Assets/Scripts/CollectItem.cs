using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	//public float flashSpeed = 5f;
	public Color blank = new Color (0f, 0f, 0f, 0f);
	public GameObject Key;
	public GameObject Gun;
	public GameObject Gloves;
	public Inventory inventory;
	bool onKey;
	bool onGun;
	bool onGloves;
	bool holdStill;
	public bool keyIsGot;
	public bool gunIsGot;
	public bool keySwap;
	public bool gunSwap;

	void Awake () 
	{
		inventory = GetComponent<Inventory>();
	}
	//WHEN YOU STEP ONTO THE OBJECT
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Key") 
		{
			pressE.color = pickupText;
			onKey = true;
		} 
		if (other.gameObject.tag == "Gun")
		{
			pressE.color = pickupText;
			onGun = true;
		}
		if (other.gameObject.tag == "Gloves")
		{
			pressE.color = pickupText;
			onGloves = true;
		}
	}
	//WHEN YOU LEAVE THAT OBJECT ON THE GROUND
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Key") 
		{
			pressE.color = blank;
			onKey = false;
		}
		if (other.gameObject.tag == "Gun") 
		{
			pressE.color = blank;
			onGun = false;
		}
		if (other.gameObject.tag == "Gloves") 
		{
			pressE.color = blank;
			onGloves = false;
		}
	}


	void Update()
	{
		if(inventory.showInventory)
		{
			holdStill = true;
			pressE.color = blank;
		}
		else
		{
			holdStill = false;
		}
		//ALLOWING ONLY ONE ITEM TO BE IN USE AT A TIME
		if(keyIsGot && inventory.activeKey == false && inventory.activeGun && gunIsGot && (Time.time - inventory.lastTapTime) < inventory.tapSpeed)
		{
			inventory.holdingKey.gameObject.SetActive(false);
			inventory.activeKey = true;
			inventory.activeGun = false;
			gunSwap = true;
		}
		if(gunIsGot && inventory.activeGun == false && inventory.activeKey && keyIsGot && (Time.time - inventory.lastTapTime) < inventory.tapSpeed)
		{
			inventory.holdingGun.gameObject.SetActive(false);
			inventory.activeGun = false;
			inventory.activeKey = true;
			keySwap = true;
			print("Adding Key");
		}
		if(gunSwap)
		{
			gunSwap = false;
			inventory.AddItem(10);
			print("Adding Gun");
		}
		if(keySwap)
		{
			keySwap = false;
			inventory.AddItem(1);
			print("Adding Key");
		}
		//RETURNING THE ITEM IF YOU DIDN'T USE IT PROPERLY AND YOU ARE NOT STANDING ON AN ITEM
		if(inventory.activeKey == true && Input.GetKeyDown("e") && onGun == false && onGloves == false)
		{
			//print("Return Key");
			inventory.holdingKey.gameObject.SetActive(false);
			inventory.AddItem(1);
			inventory.activeKey = false;
		}
	
		//GRABBING THE ITEMS
		if (onKey && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(1);
			//print ("Add Item 1");
			pressE.color = blank;
			onKey = false;
			keyIsGot = true;
			Key.gameObject.SetActive(false);
		}
		if (onGun && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(10);
			//print ("Add Item 10");
			pressE.color = blank;
			onGun = false;
			gunIsGot = true;
			Gun.gameObject.SetActive(false);
		}
		if (onGloves && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(2);
			//print ("Add Item 2");
			pressE.color = blank;
			onGloves = false;
			Gloves.gameObject.SetActive(false);
		}
	}
}

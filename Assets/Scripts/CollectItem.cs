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
	public OpenDoor openDoor;
	bool onKey;
	bool onGun;
	bool onGloves;
	bool holdStill;
	public bool keyIsGot;
	public bool gunIsGot;

	void Awake () 
	{
		inventory = GetComponent<Inventory>();
		openDoor = GameObject.FindGameObjectWithTag ("Door").GetComponent<OpenDoor> ();
		Key = GameObject.FindGameObjectWithTag ("PickUpKey");
		Gun = GameObject.FindGameObjectWithTag ("PickUpGun");
		Gloves = GameObject.FindGameObjectWithTag ("PickUpGloves");
	}
	//WHEN YOU STEP ONTO THE OBJECT
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "PickUpKey") 
		{
			pressE.color = pickupText;
			onKey = true;
		} 
		if (other.gameObject.tag == "PickUpGun")
		{
			pressE.color = pickupText;
			onGun = true;
		}
		if (other.gameObject.tag == "PickUpGloves")
		{
			pressE.color = pickupText;
			onGloves = true;
		}
	}
	//WHEN YOU LEAVE THAT OBJECT ON THE GROUND
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "PickUpKey") 
		{
			pressE.color = blank;
			onKey = false;
		}
		if (other.gameObject.tag == "PickUpGun") 
		{
			pressE.color = blank;
			onGun = false;
		}
		if (other.gameObject.tag == "PickUpGloves") 
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
		//RETURNING THE ITEM IF YOU DIDN'T USE IT PROPERLY AND YOU ARE NOT STANDING ON AN ITEM
		if(inventory.activeKey == true && Input.GetKeyDown("e") && onGun == false && onGloves == false && openDoor.haveKey == false)
		{
			//print("Return Key");
			inventory.holdingKey.gameObject.SetActive(false);
			inventory.AddItem(1);
			inventory.activeKey = false;
			inventory.keySwap = false;
			inventory.gunSwap = false;
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

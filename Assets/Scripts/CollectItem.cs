﻿using UnityEngine;
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
	public GameObject Rock;
	public GameObject Head;
	public GameObject Lung;
	public GameObject Heart;
	public GameObject Brain;
	public GameObject Ticket;
	public GameObject Poison;

	public Inventory inventory;
	public OpenDoor openDoor;
	bool onItem;
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
		Rock = GameObject.FindGameObjectWithTag ("PickUpRock");
		Head = GameObject.FindGameObjectWithTag ("PickUpHead");
		Lung = GameObject.FindGameObjectWithTag ("PickUpLung");
		Heart = GameObject.FindGameObjectWithTag ("PickUpHeart");
		Brain = GameObject.FindGameObjectWithTag ("PickUpBrain");
		Ticket = GameObject.FindGameObjectWithTag ("PickUpTicket");
		Poison = GameObject.FindGameObjectWithTag ("PickUpPoison");
	}
	//WHEN YOU STEP ONTO THE OBJECT
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "PickUpKey" || other.gameObject.tag == "PickUpGun" || other.gameObject.tag == "PickUpGloves" || other.gameObject.tag == "PickUpRock"
		    || other.gameObject.tag == "PickUpHead" || other.gameObject.tag == "PickUpLung" || other.gameObject.tag == "PickUpHeart" || other.gameObject.tag == "PickUpBrain"
		    || other.gameObject.tag == "PickUpTicket" || other.gameObject.tag == "PickUpPoison") 
		{
			pressE.color = pickupText;
			onItem = true;
			if(other.gameObject.tag == "PickUpKey")
			{
				onKey = true;
			}
			if(other.gameObject.tag == "PickUpGun")
			{
				onGun = true;
			}
			if(other.gameObject.tag == "PickUpGloves")
			{
				onGloves = true;
			}
		} 
	}
	//WHEN YOU LEAVE THAT OBJECT ON THE GROUND
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "PickUpKey" || other.gameObject.tag == "PickUpGun" || other.gameObject.tag == "PickUpGloves" || other.gameObject.tag == "PickUpRock"
		    || other.gameObject.tag == "PickUpHead" || other.gameObject.tag == "PickUpLung" || other.gameObject.tag == "PickUpHeart" || other.gameObject.tag == "PickUpBrain"
		    || other.gameObject.tag == "PickUpTicket" || other.gameObject.tag == "PickUpPoison") 
		{
			pressE.color = blank;
			onItem = false;
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
		if(inventory.activeKey == true && Input.GetKeyDown("e") && onItem == false && openDoor.haveKey == false)
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

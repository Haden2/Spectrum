﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	//public float flashSpeed = 5f;
	public Color blank = new Color (0f, 0f, 0f, 0f);
	public GameObject Key;
	public GameObject HoldingKey;
	public GameObject Gun;
	public GameObject Gloves;
	public Inventory inventory;
	bool onKey;
	bool onGun;
	bool onGloves;
	bool holdStill;

	void Awake () 
	{
		inventory = GetComponent<Inventory>();
	}

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
		if(inventory.activeKey == true && Input.GetKeyDown("e") && onGun == false && onGloves == false)
		{
			//print("Return Key");
			HoldingKey.gameObject.SetActive(false);
			inventory.AddItem(1);
		}

		if (onKey && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(1);
			//print ("Add Item 1");
			pressE.color = blank;
			onKey = false;
			Key.gameObject.SetActive(false);
		}
		if (onGun && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(10);
			//print ("Add Item 10");
			pressE.color = blank;
			onGun = false;
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

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	public Texture2D backgroundScreen;
	public float fAlpha;
	public float tapSpeed = .25f;
	private float lastTapTime = 0;
	private bool showInventory;
	private ItemDatabase database;
	private bool showTooltip;
	private string tooltip;
	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;

	void Start () 
	{
		for(int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> (); //Grab the ItemDatabase script
		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
		AddItem (5);
		AddItem (6);
		AddItem (7);
		AddItem (8);
		AddItem (9);
		//RemoveItem (0);
		lastTapTime = 0;
		

	//	print (InventoryContains(1)); //How many items are in the inventory?
	}

	void Update()
	{
		if(Input.GetButtonDown("Inventory"))
		{
			showInventory = !showInventory;
		}
		if(Input.GetKeyDown (KeyCode.Mouse0))
		{
			lastTapTime = Time.time;
		}
	}

	void OnGUI () 
	{
		if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
			SaveInventory();
		if (GUI.Button(new Rect(40,450,100,40), "Load"))
			LoadInventory();
		tooltip = "";
		GUI.skin = skin;
		if(showInventory)
		{
			DrawInventory();
			/*var colPreviousGUIColor = GUI.color;
			GUI.color = new Color(colPreviousGUIColor.r, colPreviousGUIColor.g, colPreviousGUIColor.b, fAlpha);
			GUI.DrawTexture(new Rect(0.0F, 0.0F, Screen.width, Screen.height), backgroundScreen);*/
			if(showTooltip)
			{
				GUI.Box (new Rect(Event.current.mousePosition.x /*+ 5f*/, Event.current.mousePosition.y, 150,50), tooltip, skin.GetStyle("Tooltip"));
			}
		}
		if(draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50,50), draggedItem.itemIcon);
		}
	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;

		for(int y = 0; y < slotsY; y++)
		{
			for(int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect (x*110, y *110, 100, 100);
				GUI.Box (slotRect, "", skin.GetStyle("Slot Background"));
				slots[i] = inventory[i];
				Item item = slots[i];  
				if(slots[i].itemName !=null)
				{
					GUI.DrawTexture(slotRect, inventory[i].itemIcon);
					//if(Event.current.button)
					if(slotRect.Contains(e.mousePosition))
					{
						tooltip = CreateTooltip(slots[i]);
						showTooltip = true;
						if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)//left mouse button and dragging the mouse
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = item;
							inventory[i] = new Item();
							print ("Dragging Item");
						}
						if(e.type == EventType.mouseUp && draggingItem)
						{
							inventory [prevIndex] = item;
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
						if(!draggingItem)
						{
							tooltip = CreateTooltip(inventory[i]);
							showTooltip = true;
						}
						if(e.isMouse && e.type == EventType.mouseDown && e.button == 0 && (Time.time - lastTapTime) < tapSpeed)
						{
							if(item.itemType == Item.ItemType.Key)
							{
								UseKey(item, i, true);
							}
						}
					}
					if(tooltip == "")
					{
						showTooltip = false;
					}
				} else{
					if(slotRect.Contains (e.mousePosition))
					{
						if(e.type == EventType.mouseUp && draggingItem)
						{
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}

				}

				i++;
			}
		}
	}

	string CreateTooltip(Item item)
	{
		tooltip = "<color=#0E4F69>" + item.itemName + "</color>\n\n" /* + "<color=#C4C4C4>" + item.itemDesc + "</color>"*/;
		return tooltip;
	}

	void RemoveItem (int id)
	{
		for(int i = 0; i < inventory.Count; i++)
		{
			if(inventory[i].itemID == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}

	void AddItem(int id)
	{
		for(int i =0; i < inventory.Count; i++)
		{
			if(inventory[i].itemName == null)
			{
				for(int j = 0; j < database.items.Count; j++)
				{
					if(database.items[j].itemID == id)
					{
						inventory[i] = database.items[j];
					}

				}
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		foreach(Item item in inventory)
		{
			if(item.itemID == id) return true;
		}
		return false;
	}

	private void UseKey(Item item, int slot, bool deleteItem)
	{
		switch(item.itemID)
		{
		case 1:
		{
			print ("Item in use: " + item.itemName);
			break;
		}

		}
		if(deleteItem)
		{
			inventory[slot] = new Item();
		}
	}

	void SaveInventory()
	{
		for(int i = 0; i<inventory.Count; i++)
			PlayerPrefs.SetInt ("Inventory " + i, inventory[i].itemID);
	}

	void LoadInventory()
	{
		for(int i = 0; i<inventory.Count; i++)
			inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >=0 ? database.items[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
	}
}
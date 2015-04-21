using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public int slotsX, slotsY; //5, 2
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	public Texture2D backgroundScreen;
	public float fAlpha;
	public float tapSpeed = .25f;

	public GameObject holdingKey;
	public GameObject holdingGun;
	public GameObject holdingRock;
	public GameObject [] holdingGloves;
	public GameObject holdingPoisonHead;
	public GameObject holdingAnatomy;
	public GameObject holdingTicket;

	public bool pause;
	public bool unPause;

	public bool activeKey;
	public bool activeGun;
	public bool activeRock;
	public bool activeGloves;
	public bool activePoisonHead;
	public bool activeAnatomy;
	public bool activeTicket;

	public bool keySwap;
	public bool gunSwap;
	public bool rockSwap;
	public bool poisonheadSwap;
	public bool anatomySwap;
	public bool ticketSwap;
	public bool glovesSwap;

	private MouseLook playerLook;
	private MouseLook playerCameraLook;
	public float lastTapTime = 0;
	public bool showInventory;
	private ItemDatabase database;
	public CollectItem collect;
	private bool showTooltip;
	private string tooltip;
	private bool draggingItem;
	public bool draggingLung;
	public bool draggingHeart;
	public bool draggingBrain;
	public bool draggingCombine;
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
		playerLook = (MouseLook)GameObject.Find ("First Person Controller").GetComponent ("MouseLook");
		collect = GetComponent<CollectItem>();
		holdingKey = GameObject.FindGameObjectWithTag ("Key");
		holdingGun = GameObject.FindGameObjectWithTag ("Gun");
		holdingGloves = GameObject.FindGameObjectsWithTag ("Gloves");
		holdingRock = GameObject.FindGameObjectWithTag ("Rock");
		holdingPoisonHead = GameObject.FindGameObjectWithTag ("PoisonHead");
		holdingAnatomy = GameObject.FindGameObjectWithTag ("Anatomy");
		holdingTicket = GameObject.FindGameObjectWithTag ("Ticket");


		playerCameraLook = (MouseLook)GameObject.Find ("Main Camera").GetComponent ("MouseLook");
		//AddItem (0);
		//RemoveItem (0);
		lastTapTime = 0;
		tapSpeed = .25f;
		holdingKey.SetActive (false);
		holdingGun.SetActive (false);
		holdingRock.SetActive (false);
		foreach(GameObject _obj in holdingGloves)
		{
			_obj.SetActive(false);
		}
		holdingAnatomy.SetActive (false);
		holdingPoisonHead.SetActive (false);
		holdingTicket.SetActive (false);

		pause = false;
		unPause = true;
		activeKey = false;
		activeGun = false;
		

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
			lastTapTime = Time.realtimeSinceStartup;
		}
		if(pause)
		{
			StartCoroutine(PauseGame());
		}
		if(unPause)
		{
			StartCoroutine(UnPauseGame());
		}
		if(activeKey)
		{
			keySwap = true;
		}
		if(activeGun)
		{
			gunSwap = true;
		}
		if(activeRock)
		{
			rockSwap = true;
		}
		if(activeGloves)
		{
			glovesSwap = true;
		}		
		if(activeAnatomy)
		{
			anatomySwap = true;
		}
		if(activePoisonHead)
		{
			poisonheadSwap = true;
		}
		if(activeTicket)
		{
			ticketSwap = true;
		}
		if (activeAnatomy == false)
		{
			holdingAnatomy.SetActive (false);
		}
		if(activeGloves == false)
		{
			foreach(GameObject _obj in holdingGloves)
			{
				_obj.SetActive(false);
			}
		}
		if (activeGun == false)
		{
			holdingGun.SetActive (false);
		}
		if (activeKey == false)
		{
			holdingKey.SetActive (false);
		}
		if (activePoisonHead == false)
		{
			holdingPoisonHead.SetActive (false);
		}
		if (activeRock == false)
		{
			holdingRock.SetActive (false);
		}
		if (activeTicket == false)
		{
			holdingTicket.SetActive (false);
		}
	}

	void OnGUI () 
	{
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
			if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
				SaveInventory();
			if (GUI.Button(new Rect(40,450,100,40), "Load"))
			{
				LoadInventory();
			}
			pause = true;
		}
		else
		{
			unPause = true;
			pause = false;
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
						if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem)//left mouse button and dragging the mouse. Drags item
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = item;
							inventory[i] = new Item();
							if(item.itemType == Item.ItemType.Combine)
							{
								draggingCombine = true;
								print ("Dragging Combine");
								if(item.itemID == 5)
								{
									draggingLung = true;
									draggingHeart = false;
									draggingBrain = false;
								}
								if(item.itemID == 6)
								{
									draggingLung = false;
									draggingHeart = true;
									draggingBrain = false;
								}
								if(item.itemID == 7)
								{
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = true;
								}
							}
							if(item.itemType == Item.ItemType.Key || item.itemType == Item.ItemType.Reuse || item.itemType == Item.ItemType.Vital || item.itemType == Item.ItemType.Weapon)
							{
								draggingCombine = false;
							}
						}
						if(e.type == EventType.mouseUp && draggingItem) //Dropped item on another item
						{
							inventory [prevIndex] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							if(draggingCombine && item.itemType == Item.ItemType.Combine)
							{
								if(draggingBrain == false && draggingHeart == false && draggingLung == false && item.itemID == 4 || item.itemID == 9)
								{
									RemoveItem(4);
									RemoveItem(9);
									AddItem(11);
									print ("Combine");
								}
								if(draggingCombine && draggingLung == true && item.itemID ==  6)
								{
									print ("Combined Heart and Lung");
								}
								if(draggingCombine && draggingLung == true && item.itemID ==  7)
								{
									print ("Combined Lung and Brain");
								}
								if(draggingCombine && draggingHeart == true && item.itemID ==  5)
								{
									print ("Combined Heart and Lung");
								}
								if(draggingCombine && draggingHeart == true && item.itemID ==  7)
								{
									print ("Combined Brain and Heart");
								}
								if(draggingCombine && draggingBrain == true && item.itemID ==  5)
								{
									print ("Combined Lung and Brain");
								}
								if(draggingCombine && draggingBrain == true && item.itemID ==  6)
								{
									print ("Combined Brain and Heart");
								}
								//inventory [prevIndex] = draggedItem;
							}
						}
						/*if(e.type == EventType.mouseUp && draggingItem && item.itemType == Item.ItemType.Combine)
						{
							print ("Dragging Head");
							inventory [prevIndex] = item;
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}*/
						if(!draggingItem) //HOVERING OVER ITEM
						{
							tooltip = CreateTooltip(inventory[i]);
							showTooltip = true;
							//print (item.itemType);
						}
						if(e.isMouse && e.type == EventType.mouseDown && e.button == 0 && (Time.realtimeSinceStartup - lastTapTime) < tapSpeed)
						{
							if(item.itemType == Item.ItemType.Key || item.itemType == Item.ItemType.Vital || item.itemType == Item.ItemType.Reuse || item.itemType == Item.ItemType.Weapon)
							{
								UseItem(item, i, true);
							}
							if(item.itemType == Item.ItemType.Combine)
							{
								CantUseItem(item, i, false);
							}
						}
					}
					if(tooltip == "") //Inventory is open and has Item
					{
						showTooltip = false;
						unPause = false;
					}
				} else{
					if(slotRect.Contains (e.mousePosition))
					{
						if(e.type == EventType.mouseUp && draggingItem) //Item was let go in same spot
						{
							inventory [prevIndex] = draggedItem;
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
		tooltip = "<color=#0E4F69>" + item.itemName + "</color>\n\n"  /*+ "<color=#C4C4C4>" + item.itemDesc + "</color>"*/;
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

	public void AddItem(int id)
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

	private void UseItem(Item item, int slot, bool deleteItem)
	{
		switch(item.itemID)
		{
		case 0: //Rock
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = false;
			activeGun = false;
			activeKey = false;
			activePoisonHead = false;
			activeRock = true;
			activeTicket = false;

			showInventory = false;
			holdingRock.gameObject.SetActive(true);
			if(activeRock == true && collect.gunIsGot && gunSwap)
			{
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}
			break;
		}
		case 1: //Key
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = false;
			activeGun = false;
			activeKey = true;
			activePoisonHead = false;
			activeRock = false;
			activeTicket = false;

			showInventory = false;
			holdingKey.gameObject.SetActive(true);
			if(activeKey == true && collect.gunIsGot && gunSwap)
			{
				gunSwap = false;
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}
			if(activeKey == true && collect.ticketIsGot && ticketSwap)
			{
				ticketSwap = false;
				holdingTicket.gameObject.SetActive(false);
				AddItem(8);
			}
			if(activeKey == true && collect.rockIsGot && rockSwap)
			{
				rockSwap = false;
				holdingRock.gameObject.SetActive(false);
				AddItem(0);
			}
			if(activeKey == true && collect.glovesIsGot && glovesSwap)
			{
				glovesSwap = false;
				foreach(GameObject _obj in holdingGloves)
				{
					_obj.SetActive(false);
				}
				AddItem(2);
			}
			/*if(activeKey == true && collect.IsGot && rockSwap)
			{
				rockSwap = false;
				holdingRock.gameObject.SetActive(false);
				AddItem(0);
			}*/
			break;
		}
		case 2: //Gloves
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = true;
			activeGun = false;
			activeKey = false;
			activePoisonHead = false;
			activeRock = false;
			activeTicket = false;

			showInventory = false;
			foreach(GameObject _obj in holdingGloves)
			{
				_obj.SetActive(true);
			}
			/*if(activeKey == true && collect.gunIsGot && gunSwap)
			{
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}*/
			break;
		}
		case 8: //Ticket
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = false;
			activeGun = false;
			activeKey = false;
			activePoisonHead = false;
			activeRock = false;
			activeTicket = true;

			showInventory = false;
			holdingTicket.gameObject.SetActive(true);
			/*if(activeKey == true && collect.gunIsGot && gunSwap)
			{
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}*/
			break;
		}
		case 10: //Gun
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = false;
			activeGun = true;
			activeKey = false;
			activePoisonHead = false;
			activeRock = false;
			activeTicket = false;

			showInventory = false;
			holdingGun.gameObject.SetActive(true);
		/*	if(activeGun == true && collect.keyIsGot && keySwap)
			{
				holdingKey.gameObject.SetActive(false);
				AddItem(1);
			}*/
			break;
		}
		case 11: //Poisoned Head
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = false;
			activeGloves = false;
			activeGun = false;
			activeKey = false;
			activePoisonHead = true;
			activeRock = false;
			activeTicket = false;

			showInventory = false;
			holdingPoisonHead.gameObject.SetActive(true);
			/*if(activeKey == true && collect.gunIsGot && gunSwap)
			{
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}*/
			break;
		}
		case 12: //Anatomy
		{
			print ("Item in use: " + item.itemName);
			activeAnatomy = true;
			activeGloves = false;
			activeGun = false;
			activeKey = false;
			activePoisonHead = false;
			activeRock = false;
			activeTicket = false;

			showInventory = false;
			holdingAnatomy.gameObject.SetActive(true);
			/*if(activeKey == true && collect.gunIsGot && gunSwap)
			{
				holdingGun.gameObject.SetActive(false);
				AddItem(10);
			}*/
			break;
		}
		}
		if(deleteItem)
		{
			inventory[slot] = new Item();
		}
	}

	private void CantUseItem(Item item, int slot, bool deleteItem)
	{
		switch(item.itemID)
		{
		case 4: //Severed Head
		{
			print ("Item needs to be combined with Poison ");
			showInventory = true;
			break;
		}
		case 5: //Lung
		{
			print ("Item needs to be combined with Brain and Heart ");
			showInventory = true;
			break;
		}
		case 6: //Heart
		{
			print ("Item needs to be combined with Brain and Lung ");
			showInventory = true;
			break;
		}
		case 7: //Brain
		{
			print ("Item needs to be combined with Heart and Lung ");
			showInventory = true;
			break;
		}
		case 9: //Poison
		{
			print ("Item needs to be combined with Severed Head ");
			showInventory = true;
			break;
		}
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

	IEnumerator PauseGame()
	{
		Time.timeScale = 0.0f;
		gameObject.GetComponent<MouseLook>().enabled = false;
		playerLook.GetComponent<MouseLook>().enabled = false;
		playerCameraLook.GetComponent<MouseLook>().enabled = false;
		Cursor.visible = true;
		yield return new WaitForSeconds (0);
		pause = true;
		unPause = false;
	}

	IEnumerator UnPauseGame()
	{
		unPause = true;
		pause = false;
		Cursor.visible = false;
		gameObject.GetComponent<MouseLook>().enabled = true;
		playerLook.GetComponent<MouseLook>().enabled = true;
		playerCameraLook.GetComponent<MouseLook>().enabled = true;
		Time.timeScale = 1.0f;
		yield return new WaitForSeconds (0);
	}
}

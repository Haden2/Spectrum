using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public string tooltip;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	//public Texture2D backgroundScreen;
	
	public GameObject holdingKey;
	public GameObject holdingGun;
	public GameObject holdingRock;
	public GameObject [] holdingGloves;
	public GameObject holdingPoisonHead;
	public GameObject holdingAnatomy;
	public GameObject holdingTicket;
	public GameObject holdingElevatorKey;

	public bool pause;
	public bool unPause;
	public bool activeKey;
	public bool activeGun;
	public bool activeRock;
	public bool activeGloves;
	public bool activePoisonHead;
	public bool activeAnatomy;
	public bool activeTicket;
	public bool activeElevatorKey;
	public bool keySwap;
	public bool gunSwap;
	public bool rockSwap;
	public bool poisonheadSwap;
	public bool anatomySwap;
	public bool ticketSwap;
	public bool glovesSwap;
	public bool elevatorkeySwap;
	public bool draggingItem;
	public bool draggingLung;
	public bool draggingHeart;
	public bool draggingBrain;
	public bool draggingHead;
	public bool draggingPoison;
	public bool draggingBrainHeart;
	public bool draggingBrainLung;
	public bool draggingHeartLung;
	public bool draggingCombine;
	public bool draggingElevatorKey;
	public bool firstShot;
	public bool firstThrow;
	public bool showTooltip;
	public bool showInventory;

	public int slotsX, slotsY; //5, 2
	public int prevIndex;
	public float lastTapTime = 0;
	public float fAlpha;
	public float tapSpeed;

	public MouseLook playerLook;
	public MouseLook playerCameraLook;
	public ItemDatabase database;
	public CollectItem collect;
	public HospitalGirl enemyDamage;
	public Item draggedItem;

	void Start () 
	{
		for(int i = 0; i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add (new Item());
		}
		holdingKey = GameObject.Find ("HoldingKey");
		holdingGun = GameObject.Find ("HoldingGun");
		holdingGloves = GameObject.FindGameObjectsWithTag ("Gloves");
		holdingRock = GameObject.Find ("HoldingRock");
		holdingPoisonHead = GameObject.Find ("HoldingPoisonHead");
		holdingAnatomy = GameObject.Find ("HoldingAnatomy");
		holdingTicket = GameObject.Find ("HoldingTicket");
		holdingElevatorKey = GameObject.Find ("HoldingElevatorKey");

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
		holdingElevatorKey.SetActive (false);

		unPause = true;

		slotsX = 6;
		slotsY = 2;
		lastTapTime = 0;
		tapSpeed = .25f;

		playerLook = (MouseLook)GameObject.Find ("First Person Controller").GetComponent ("MouseLook");
		playerCameraLook = (MouseLook)GameObject.Find ("Main Camera").GetComponent ("MouseLook");
		database = GameObject.Find ("Item Database").GetComponent<ItemDatabase> ();
		collect = GetComponent<CollectItem>();
		enemyDamage = GameObject.Find ("HospitalGirl").GetComponent<HospitalGirl> (); 	
		//AddItem (0);
		//RemoveItem (0);
		//print (InventoryContains(1)); //How many items are in the inventory?
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
			firstShot = true;
		}
		if(activeRock)
		{
			rockSwap = true;
			firstThrow = true;
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
		if(activeElevatorKey)
		{
			elevatorkeySwap = true;
		}
		////////////////////////////////
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
			firstShot = false;
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
			firstThrow = false;
		}
		if (activeTicket == false)
		{
			holdingTicket.SetActive (false);
		}
		if (activeElevatorKey == false)
		{
			holdingElevatorKey.SetActive (false);
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
								//print ("Dragging Combine");
								if(item.itemID == 4)
								{
									draggingHead = true;
									draggingPoison = false;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 9)
								{
									draggingHead = false;
									draggingPoison = true;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 5)
								{
									draggingLung = true;
									draggingHeart = false;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 6)
								{
									draggingLung = false;
									draggingHeart = true;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 7)
								{
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = true;
									draggingHead = false;
									draggingPoison = false;
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 13)
								{
									draggingHeartLung = false;
									draggingBrainLung = false;
									draggingBrainHeart = true;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 14)
								{
									draggingBrainLung = true;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingHeartLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 15)
								{
									draggingHeartLung = true;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = false;
								}
								if(item.itemID == 16)
								{
									draggingHeartLung = false;
									draggingLung = false;
									draggingHeart = false;
									draggingBrain = false;
									draggingHead = false;
									draggingPoison = false;
									draggingBrainLung = false;
									draggingBrainHeart = false;
									draggingElevatorKey = true;
								}
							}
							if(item.itemType == Item.ItemType.Key || item.itemType == Item.ItemType.Reuse || item.itemType == Item.ItemType.Vital || item.itemType == Item.ItemType.Weapon)
							{
								draggingCombine = false;
								draggingLung = false;
								draggingHeart = false;
								draggingBrain = false;
								draggingHead = false;
								draggingPoison = false;
								draggingHeartLung = false;
								draggingBrainLung = false;
								draggingBrainHeart = false;
							}
						}
						if(e.type == EventType.mouseUp && draggingItem) //Dropped item on another item
						{
							inventory [prevIndex] = draggedItem;
							draggingItem = false;
							draggedItem = null;
							if(draggingCombine && item.itemType == Item.ItemType.Combine)
							{
								if((draggingHead || draggingPoison) && (item.itemID == 4 || item.itemID == 9))
								{
									RemoveItem(4);
									RemoveItem(9);
									AddItem(11);
									collect.poisonheadIsGot = true;
									//print ("Combine");
								}
								if(draggingCombine && (draggingLung || draggingHeart) && (item.itemID ==  6 || item.itemID == 5))
								{
									//print ("Combined Heart and Lung");
									RemoveItem(6);
									RemoveItem(5);
									AddItem(15);
								}
								if(draggingCombine && (draggingLung || draggingBrain) && (item.itemID ==  7 || item.itemID == 5))
								{
									//print ("Combined Lung and Brain");
									RemoveItem(7);
									RemoveItem(5);
									AddItem(14);
								}
								if(draggingCombine && (draggingHeart || draggingBrain) && (item.itemID ==  7 || item.itemID == 6))
								{
									//print ("Combined Brain and Heart");
									RemoveItem(7);
									RemoveItem(6);
									AddItem(13);
								}
								if(draggingCombine && (draggingBrainHeart || draggingLung) && (item.itemID == 13 || item.itemID == 5))
								{
									RemoveItem(13);
									RemoveItem(5);
									AddItem(12);
									collect.anatomyIsGot = true;
								}
								if(draggingCombine && (draggingBrainLung || draggingHeart) && (item.itemID == 14 || item.itemID == 6))
								{
									RemoveItem(14);
									RemoveItem(6);
									AddItem(12);
									collect.anatomyIsGot = true;
								}
								if(draggingCombine && (draggingHeartLung || draggingBrain) && (item.itemID == 15 || item.itemID == 7))
								{
									RemoveItem(15);
									RemoveItem(7);
									AddItem(12);
									collect.anatomyIsGot = true;
								}
							}
						}
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
		switch (item.itemID) {
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
				activeElevatorKey = false;

				showInventory = false;
				holdingRock.gameObject.SetActive (true);
				if (activeRock == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeRock == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeRock == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeRock == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeRock == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeRock == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activeRock == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
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
				activeElevatorKey = false;

				showInventory = false;
				holdingKey.gameObject.SetActive (true);
				if (activeKey == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeKey == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeKey == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeKey == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeKey == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeKey == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activeKey == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
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
				activeElevatorKey = false;

				showInventory = false;
				foreach (GameObject _obj in holdingGloves) {
					_obj.SetActive (true);
				}
				if (activeGloves == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeGloves == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeGloves == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeGloves == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeGloves == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeGloves == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activeGloves == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
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
				activeElevatorKey = false;

				showInventory = false;
				holdingTicket.gameObject.SetActive (true);
				if (activeTicket == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeTicket == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeTicket == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeTicket == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeTicket == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeTicket == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activeTicket == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
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
				activeElevatorKey = false;

				showInventory = false;
				holdingGun.gameObject.SetActive (true);
				if (activeGun == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeGun == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeGun == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeGun == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeGun == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeGun == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activeGun == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
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
				activeElevatorKey = false;

				showInventory = false;
				holdingPoisonHead.gameObject.SetActive (true);
				if (activePoisonHead == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activePoisonHead == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activePoisonHead == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activePoisonHead == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activePoisonHead == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activePoisonHead == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
				if (activePoisonHead == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
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
				activeElevatorKey = false;

				showInventory = false;
				holdingAnatomy.gameObject.SetActive (true);
				if (activeAnatomy == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeAnatomy == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeAnatomy == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeAnatomy == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeAnatomy == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeAnatomy == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeAnatomy == true && collect.elevatorkeyIsGot && elevatorkeySwap) {
					elevatorkeySwap = false;
					holdingElevatorKey.gameObject.SetActive (false);
					AddItem (16);
				}
				break;
			}
		case 16: //ElevatorKey
			{
				print ("Item in use: " + item.itemName);
				activeAnatomy = false;
				activeGloves = false;
				activeGun = false;
				activeKey = false;
				activePoisonHead = false;
				activeRock = false;
				activeTicket = false;
				activeElevatorKey = true;
				
				showInventory = false;
				holdingElevatorKey.gameObject.SetActive (true);
				if (activeElevatorKey == true && collect.rockIsGot && rockSwap) {
					rockSwap = false;
					holdingRock.gameObject.SetActive (false);
					AddItem (0);
				}
				if (activeElevatorKey == true && collect.keyIsGot && keySwap) {
					keySwap = false;
					holdingKey.gameObject.SetActive (false);
					AddItem (1);
				}
				if (activeElevatorKey == true && collect.glovesIsGot && glovesSwap) {
					glovesSwap = false;
					foreach (GameObject _obj in holdingGloves) {
						_obj.SetActive (false);
					}
					AddItem (2);
				}
				if (activeElevatorKey == true && collect.ticketIsGot && ticketSwap) {
					ticketSwap = false;
					holdingTicket.gameObject.SetActive (false);
					AddItem (8);
				}
				if (activeElevatorKey == true && collect.gunIsGot && gunSwap) {
					gunSwap = false;
					holdingGun.gameObject.SetActive (false);
					AddItem (10);
				}
				if (activeElevatorKey == true && collect.poisonheadIsGot && poisonheadSwap) {
					poisonheadSwap = false;
					holdingPoisonHead.gameObject.SetActive (false);
					AddItem (11);
				}
				if (activeElevatorKey == true && collect.anatomyIsGot && anatomySwap) {
					anatomySwap = false;
					holdingAnatomy.gameObject.SetActive (false);
					AddItem (12);
				}
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
		if(unPause /*&& enemyDamage.dontMove == false*/)
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

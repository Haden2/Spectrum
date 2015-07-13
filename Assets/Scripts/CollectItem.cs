using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
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
	public GameObject ElevatorKey;

	public bool onItem;
	public bool onKey;
	public bool onGun;
	public bool onGloves;
	public bool onRock;
	public bool onHead;
	public bool onLung;
	public bool onBrain;
	public bool onHeart;
	public bool onTicket;
	public bool onPoison;
	public bool onElevatorKey;
	public bool holdStill;

	public bool keyIsGot;
	public bool gunIsGot;
	public bool glovesIsGot;
	public bool rockIsGot;
	public bool headIsGot;
	public bool lungIsGot;
	public bool heartIsGot;
	public bool brainIsGot;
	public bool ticketIsGot;
	public bool poisonIsGot;
	public bool poisonheadIsGot;
	public bool anatomyIsGot;
	public bool elevatorkeyIsGot;

	public Inventory inventory;
	public Environmental environment;

	void Awake () 
	{
		Key = GameObject.Find ("Key");
		Gun = GameObject.Find ("Gun");
		Gloves = GameObject.Find ("Gloves");
		Rock = GameObject.Find ("Rock");
		Head = GameObject.Find ("Head");
		Lung = GameObject.Find ("Lung");
		Heart = GameObject.Find ("Heart");
		Brain = GameObject.Find ("Brain");
		Ticket = GameObject.Find ("Ticket");
		Poison = GameObject.Find ("Poison");
		ElevatorKey = GameObject.Find ("ElevatorKey");

		environment = GameObject.Find ("Environment").GetComponent<Environmental> ();
		inventory = GetComponent<Inventory>();
	}
	//WHEN YOU STEP ONTO THE OBJECT
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Item") 
		{
			pressE.color = pickupText;
			onItem = true;
			if(other.gameObject.name == "Rock")
			{
				onRock = true;
			}
			if(other.gameObject.name == "Key")
			{
				onKey = true;
			}
			if(other.gameObject.name == "Gun")
			{
				onGun = true;
			}
			if(other.gameObject.name == "Gloves")
			{
				onGloves = true;
			}
			if(other.gameObject.name == "Head")
			{
				onHead = true;
			}
			if(other.gameObject.name == "Lung")
			{
				onLung = true;
			}
			if(other.gameObject.name == "Heart")
			{
				onHeart = true;
			}
			if(other.gameObject.name == "Brain")
			{
				onBrain = true;
			}
			if(other.gameObject.name == "Ticket")
			{
				onTicket = true;
			}
			if(other.gameObject.name == "Poison")
			{
				onPoison = true;
			}
			if(other.gameObject.name == "ElevatorKey")
			{
				onElevatorKey = true;
			}
		} 
	}
	//WHEN YOU LEAVE THAT OBJECT ON THE GROUND
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Item") 
		{
			pressE.color = blank;
			onItem = false;
			if(other.gameObject.name == "Rock")
			{
				onRock = false;
			}
			if(other.gameObject.name == "Key")
			{
				onKey = false;
			}
			if(other.gameObject.name == "Gun")
			{
				onGun = false;
			}
			if(other.gameObject.name == "Gloves")
			{
				onGloves = false;
			}
			if(other.gameObject.name == "Head")
			{
				onHead = false;
			}
			if(other.gameObject.name == "Lung")
			{
				onLung = false;
			}
			if(other.gameObject.name == "Heart")
			{
				onHeart = false;
			}
			if(other.gameObject.name == "Brain")
			{
				onBrain = false;
			}
			if(other.gameObject.name == "Ticket")
			{
				onTicket = false;
			}
			if(other.gameObject.name == "Poison")
			{
				onPoison = false;
			}
			if(other.gameObject.name == "ElevatorKey")
			{
				onElevatorKey = false;
			}
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
		if(inventory.activeKey == true && Input.GetKeyDown("e") && onItem == false && environment.haveKey == false)
		{
			//print("Return Key");
			inventory.holdingKey.gameObject.SetActive(false);
			inventory.AddItem(1);
			inventory.activeKey = false;
			inventory.keySwap = false;
			inventory.gunSwap = false;
			inventory.rockSwap = false;
			inventory.poisonheadSwap = false;
			inventory.anatomySwap = false;
			inventory.ticketSwap = false;
			inventory.glovesSwap = false;
			inventory.elevatorkeySwap = false;
		}
	
		//GRABBING THE ITEMS
		if (onKey && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(1);
			//print ("Add Item 1");
			pressE.color = blank;
			onKey = false;
			onItem = false;
			keyIsGot = true;
			Key.gameObject.SetActive(false);
		}
		if (onGun && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(10);
			//print ("Add Item 10");
			pressE.color = blank;
			onGun = false;
			onItem = false;
			gunIsGot = true;
			Gun.gameObject.SetActive(false);
		}
		if (onGloves && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(2);
			//print ("Add Item 2");
			pressE.color = blank;
			onGloves = false;
			onItem = false;
			glovesIsGot = true;
			Gloves.gameObject.SetActive(false);
		}
		if (onHead && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(4);
			pressE.color = blank;
			onHead = false;
			onItem = false;
			headIsGot = true;
			Head.gameObject.SetActive(false);
		}
		if (onRock && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(0);
			pressE.color = blank;
			onRock = false;
			onItem = false;
			rockIsGot = true;
			Rock.gameObject.SetActive(false);
		}
		if (onLung && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(5);
			pressE.color = blank;
			onLung = false;
			onItem = false;
			lungIsGot = true;
			Lung.gameObject.SetActive(false);
		}
		if (onHeart && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(6);
			pressE.color = blank;
			onHeart = false;
			onItem = false;
			heartIsGot = true;
			Heart.gameObject.SetActive(false);
		}
		if (onBrain && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(7);
			pressE.color = blank;
			onBrain = false;
			onItem = false;
			brainIsGot = true;
			Brain.gameObject.SetActive(false);
		}
		if (onTicket && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(8);
			pressE.color = blank;
			onTicket = false;
			onItem = false;
			ticketIsGot = true;
			Ticket.gameObject.SetActive(false);
		}
		if (onPoison && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(9);
			pressE.color = blank;
			onPoison = false;
			onItem = false;
			poisonIsGot = true;
			Poison.gameObject.SetActive(false);
		}
		if (onElevatorKey && Input.GetKeyDown ("e") && holdStill == false)
		{
			inventory.AddItem(16);
			pressE.color = blank;
			onElevatorKey = false;
			onItem = false;
			elevatorkeyIsGot = true;
			ElevatorKey.gameObject.SetActive(false);
		}
	}
}

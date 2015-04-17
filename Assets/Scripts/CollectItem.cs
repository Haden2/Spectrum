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
	public GameObject Rock;
	public GameObject Head;
	public GameObject Lung;
	public GameObject Heart;
	public GameObject Brain;
	public GameObject Ticket;
	public GameObject Poison;

	public Inventory inventory;
	public OpenDoor openDoor;

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

	bool holdStill;
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
			if(other.gameObject.tag == "PickUpRock")
			{
				onRock = true;
			}
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
			if(other.gameObject.tag == "PickUpHead")
			{
				onHead = true;
			}
			if(other.gameObject.tag == "PickUpLung")
			{
				onLung = true;
			}
			if(other.gameObject.tag == "PickUpHeart")
			{
				onHeart = true;
			}
			if(other.gameObject.tag == "PickUpBrain")
			{
				onBrain = true;
			}
			if(other.gameObject.tag == "PickUpTicket")
			{
				onTicket = true;
			}
			if(other.gameObject.tag == "PickUpPoison")
			{
				onPoison = true;
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
			if(other.gameObject.tag == "PickUpRock")
			{
				onRock = false;
			}
			if(other.gameObject.tag == "PickUpKey")
			{
				onKey = false;
			}
			if(other.gameObject.tag == "PickUpGun")
			{
				onGun = false;
			}
			if(other.gameObject.tag == "PickUpGloves")
			{
				onGloves = false;
			}
			if(other.gameObject.tag == "PickUpHead")
			{
				onHead = false;
			}
			if(other.gameObject.tag == "PickUpLung")
			{
				onLung = false;
			}
			if(other.gameObject.tag == "PickUpHeart")
			{
				onHeart = false;
			}
			if(other.gameObject.tag == "PickUpBrain")
			{
				onBrain = false;
			}
			if(other.gameObject.tag == "PickUpTicket")
			{
				onTicket = false;
			}
			if(other.gameObject.tag == "PickUpPoison")
			{
				onPoison = false;
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
	}
}

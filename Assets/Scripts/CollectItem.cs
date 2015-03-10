using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	//public float flashSpeed = 5f;
	public Color blank = new Color (0f, 0f, 0f, 0f);
	public GameObject CollectingItem;
	private Inventory inventory;
	//public ItemDatabase database;
	bool textShown;

	void Awake () 
	{
		inventory = GetComponent<Inventory>();
	//	database = GetComponent<ItemDatabase> ();

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "PickUp") 
		{
			pressE.color = pickupText;
			textShown = true;
		} 
	}
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "PickUp") 
		{
			pressE.color = blank;
			textShown = false;
		}
	}

	void Update()
	{
		if (textShown && Input.GetKeyDown ("e"))
		{
			inventory.AddItem(1);
			print ("Add Item 1");
			pressE.color = blank;
			textShown = false;
			CollectingItem.gameObject.SetActive(false);
		}
	}
}

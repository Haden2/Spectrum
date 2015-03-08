using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	//public float flashSpeed = 5f;
	public Color blank = new Color (0f, 0f, 0f, 0f);
	public GameObject CollectingItem;
	Inventory addingItem;
	ItemDatabase database;
	bool textShown;

	void Start () 
	{
		addingItem = GetComponent<Inventory>();
		database = GetComponent<ItemDatabase> ();
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
			addingItem.AddItem(5);
			print ("Add Item 1");
			CollectingItem.gameObject.SetActive(false);
			pressE.color = blank;

		}
	}
}

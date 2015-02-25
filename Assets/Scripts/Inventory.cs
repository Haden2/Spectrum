using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public List<Item> inventory = new List<Item>();
	private ItemDatabase database;

	void Start () 
	{
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> (); //Grab the ItemDatabase script
		print (inventory.Count); //How many items are in the inventory?
		inventory.Add (database.items [0]); //Add the 0 item to the inventory
		print (inventory.Count); //Now how many are in the inventory?
		//print(database.items[0].itemName);
	}

	void OnGUI () 
	{
		for(int i = 0; i < inventory.Count; i++)//i is 0, but increase i by one. Keep doing this until i is still < the inventory count
		{
			GUI.Label(new Rect(10,i * 30,200,50), inventory[i].itemName); //A square that has slots for the inventory starting at 0 and increasing. i*number makes it so each item is seperated from each other.
		}
	}
}

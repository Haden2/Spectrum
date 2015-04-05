using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	public List<Item> items = new List<Item> (); //Items is equal to a new list of items.

	void Start()
	{
		items.Add (new Item ("Rock",0,"Used to distract enemies", 1, Item.ItemType.Reuse));//Name, item ID, Description, is it resuable, and what kind of type is it?
		items.Add (new Item ("Key",1,"Unlocks doors", 0, Item.ItemType.Key));
		items.Add (new Item ("Gloves",2,"Finishes off the Jumper", 0, Item.ItemType.Vital));
		items.Add (new Item ("Matches",3,"Burns the Spiders nest", 0, Item.ItemType.Vital));
		items.Add (new Item ("Head",4,"Give to the Borrower", 0, Item.ItemType.Vital));
		items.Add (new Item ("Lung",5,"1/3 items for the Model", 0, Item.ItemType.Vital));
		items.Add (new Item ("Heart",6,"1/3 items for the Model", 0, Item.ItemType.Vital));
		items.Add (new Item ("Brain",7,"1/3 items for the Model", 0, Item.ItemType.Vital));
		items.Add (new Item ("Ticket",8,"Allows access to the Clown", 0, Item.ItemType.Vital));
		items.Add (new Item ("Poison",9,"Apply to the head to kill the Borrower", 0, Item.ItemType.Vital));
		items.Add (new Item ("Gun", 10, "Used to shoot enemies", 0, Item.ItemType.Vital));
	}
}

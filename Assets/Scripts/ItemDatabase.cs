using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour 
{
	public List<Item> items = new List<Item> (); //Items is equal to a new list of items.

	void Start()
	{
		items.Add (new Item ("Rock",0,"Used to distract enemies", 1,0, Item.ItemType.Reuse));//Name, item ID, Description, is it resuable, speed?, and what kind of type is it?
	}
}

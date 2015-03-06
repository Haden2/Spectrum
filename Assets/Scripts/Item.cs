using UnityEngine;
using System.Collections;

	[System.Serializable]
public class Item {
	public string itemName;
	public int itemID; //How many items we have. Find the number associated with the item and say, add this one to inventory
	public string itemDesc; //The note on the item
	public Texture2D itemIcon; //Draw in an icon. Jpg, png. 
	public int itemReuse; //Stats. Don't need
	public ItemType itemType;

	public enum ItemType{
		Weapon,
		Vital,
		Key,
		Reuse
	}

	public Item(string name, int id, string desc, int reuse, ItemType type)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D> ("Icons/" + name);
		itemReuse = reuse;
		itemType = type;

	}
	public Item()
	{
		itemID = -1;
	}
}

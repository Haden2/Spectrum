﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectItem : MonoBehaviour {

	public Text pressE;
	public Color pickupText = new Color(1f, 0f, 0f, 1f);
	public float flashSpeed = 5f;
	public Color blank = new Color (0f, 0f, 0f, 0f);

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "PickUp") 
		{
			pressE.color = pickupText;
		} 
		else //if (other.gameObject.tag (Doesn't equal) "PickUp")
		{
			pressE.color = blank;
		}
	}
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "PickUp") 
		{
			pressE.color = blank;
		}
	}
}

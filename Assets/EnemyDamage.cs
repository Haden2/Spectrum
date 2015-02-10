using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	GameObject player; 

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == player)
		{
			BlueFlashlight.gameObject.SetActive(false);
		}

	}


}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	GameObject player;
	public int testing12;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == player)
		{
			testing12 = 3;
		}

	}


}

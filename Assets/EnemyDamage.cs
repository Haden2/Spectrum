using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	GameObject player;
	ToggleFlashlight lightIntensity;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		lightIntensity = GetComponent <ToggleFlashlight>();
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == player)
		{
			light.intensity = 0;
		}

	}
	

}

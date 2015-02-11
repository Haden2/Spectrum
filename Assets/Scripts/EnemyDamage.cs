using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	GameObject blueLight;
	public int testing12;

	void Awake()
	{
		blueLight = GameObject.FindGameObjectWithTag ("Light");
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == blueLight)
		{
			testing12 = 3;
			other.gameObject.SetActive(false);
		}

	}


}

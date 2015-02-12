using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	GameObject player; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;

	bool escape;


	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == player)
		{
			BlueFlashlight.gameObject.SetActive(false);
			nav.SetDestination (escapeDestination.position);
			escape = true;
		}

	}

	void Update()
	{
		if(escape)
		{
			nav.SetDestination(escapeDestination.position);
			turnUp.speed = 10;
		}
	}

}

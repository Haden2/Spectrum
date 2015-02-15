using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	GameObject main; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	//Transform player;
	NavMeshPathStatus done;

	bool escape;


	void Awake()
	{
		main = GameObject.FindGameObjectWithTag ("Player");
		//player = GameObject.FindGameObjectWithTag ("Player").transform;
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
		//NavMeshPath done = NavMeshPathStatus.PathComplete;
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main)
		{
			BlueFlashlight.gameObject.SetActive(false);
			nav.SetDestination (escapeDestination.position);
			escape = true;
		}
		/*if(other.gameObject == main && done == true/escape == true)
		{
			BlueFlashlight.gameObject.SetActive(true);
			nav.SetDestination (player.position);
		}*/
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

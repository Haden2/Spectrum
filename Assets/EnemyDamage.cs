using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	public float wait;
	public float readyOrNot;
	GameObject main; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	Transform player;

	bool escape;
	bool run;
	bool seek;
	bool light;


	void Awake()
	{
		main = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
		seek = true;
		light = true;
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main && seek == true)
		{
			StartCoroutine (WaitForLight ());
		}

		if(other.gameObject == main && escape == true)
		{
			StartCoroutine (HideAndSeek ());
		}

	}

	IEnumerator WaitForLight()
	{
		light = false;
		seek = false;
		run = true;
		yield return new WaitForSeconds (wait);
		escape = true;
	}

	IEnumerator HideAndSeek()
	{
		light = true;
		escape = false;
		yield return new WaitForSeconds (readyOrNot);
		seek = true;
		run = false;
	}

	void Update()
	{
		if(run)
		{
			nav.SetDestination(escapeDestination.position);
			turnUp.speed = 10;
		}
		if(seek)
		{
			turnUp.speed = 1;
			nav.SetDestination (player.position);
		}
		if(light == true)
		{
			BlueFlashlight.gameObject.SetActive(true);
		}
		if (light == false)
		{
			BlueFlashlight.gameObject.SetActive(false);
		}
	}
}

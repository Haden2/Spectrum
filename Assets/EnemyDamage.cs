using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	public float wait;
	public float readyOrNot;
	public float startingTalk;
	GameObject main; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	Transform player;

	bool escape;
	bool run;
	bool seek;
	bool flash;
	bool start;


	void Awake()
	{
		main = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
		start = true;
		seek = false;
		flash = true;
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main && start == true && seek == false)
		{
			StartCoroutine (StartGame());
		}
		if(other.gameObject == main && seek == true && start == false)
		{
			StartCoroutine (WaitForLight ());
		}

		if(other.gameObject == main && escape == true && seek == false)
		{
			StartCoroutine (HideAndSeek ());
		}

	}

	IEnumerator StartGame()
	{
		start = false;
		yield return new WaitForSeconds (startingTalk);
		seek = true;
	}

	IEnumerator WaitForLight()
	{
		flash = false;
		seek = false;
		run = true;
		yield return new WaitForSeconds (wait);
		escape = true;
	}

	IEnumerator HideAndSeek()
	{
		flash = true;
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
		if(flash == true)
		{
			BlueFlashlight.gameObject.SetActive(true);
		}
		if (flash == false)
		{
			BlueFlashlight.gameObject.SetActive(false);
		}
		if(start == true)
		{
			turnUp.speed = 0;
		}

	}
}

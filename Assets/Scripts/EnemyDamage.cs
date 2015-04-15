using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	//TestingNightVision NightVisionLight;
	public GameObject NightVision;
	public float wait;
	public float readyOrNot;
	public float startingTalk;
	public float radius;
	GameObject main; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	Transform player;
	public SphereCollider collide;

	bool escape;
	bool run;
	bool seek;
	bool flash;
	bool start;


	void Awake()
	{
		BlueFlashlight = GameObject.FindGameObjectWithTag ("BlueLight");
		NightVision = GameObject.FindGameObjectWithTag ("NightVisionLight");
		collide = GetComponent<SphereCollider> ();
		main = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
		startingTalk = 15f;
		wait = 3;
		readyOrNot = 11;
		radius = 8;
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
		collide.radius = 1.2f;
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
			NightVision.gameObject.SetActive(false);
		}
		if(start == true)
		{
			turnUp.speed = 0;
			collide.radius = 8;
		}

	}
}

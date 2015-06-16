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
	public float endingSequence;
	public float radius;
	GameObject main; 
	GameObject mother;
	GameObject BehindPlayer;
	GameObject leftOfPlayer;
	GameObject rightOfPlayer;
	GameObject camera;
	TestingNightVision testingNight;
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	PlayerController player;
	public SphereCollider collide;
	public float behindPlayerAngle;
	public float playerAngle;
	MouseLook mouseLook;
	bool escape;
	bool run;
	bool seek;
	bool flash;
	bool start;
	public bool toBehindPlayer;
	bool jumpScare;


	void Awake()
	{
		BlueFlashlight = GameObject.FindGameObjectWithTag ("BlueLight");
		NightVision = GameObject.FindGameObjectWithTag ("NightVisionLight");
		collide = GetComponent<SphereCollider> ();
		main = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		mother = GameObject.FindGameObjectWithTag ("Mother");
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
		startingTalk = 2f; //15
		wait = 3;
		readyOrNot = 11;
		endingSequence = 2;
		radius = 8;
		start = true;
		seek = false;
		flash = true;
		testingNight = GameObject.FindGameObjectWithTag("Player").GetComponent<TestingNightVision> ();
		BehindPlayer = GameObject.Find ("BehindPlayer");
		leftOfPlayer = GameObject.Find("LeftPlayer");
		rightOfPlayer = GameObject.Find("RightPlayer");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		mouseLook = (MouseLook)GameObject.Find ("Main Camera").GetComponent ("MouseLook");
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
		if(other.gameObject == mother && run == false)
		{
			StartCoroutine (FoundMother ());
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
		behindPlayerAngle = main.transform.eulerAngles.y + 180;
		print (behindPlayerAngle);
		if(player.hGInsight)
		{
			toBehindPlayer = true;
			if(behindPlayerAngle >= 360)
			{
				behindPlayerAngle = behindPlayerAngle - 360;
				print (behindPlayerAngle);
			}

		}
		//flash = false;
		seek = false;
		//run = true;
		yield return new WaitForSeconds (wait);
		//escape = true;
	}

	IEnumerator HideAndSeek()
	{
		flash = true;
		escape = false;
		yield return new WaitForSeconds (readyOrNot);
		seek = true;
		run = false;
	}
	IEnumerator FoundMother()
	{
		seek = false;
		escape = false;
		run = false;
		turnUp.speed = 0; 
		print ("Found Mother");
		yield return new WaitForSeconds (endingSequence);
		gameObject.SetActive (false);
	}

	void Update()
	{
		playerAngle = main.transform.eulerAngles.y;
		if(((playerAngle < behindPlayerAngle + 90) && playerAngle > behindPlayerAngle) && toBehindPlayer || (playerAngle > behindPlayerAngle - 90) && (playerAngle < behindPlayerAngle) && toBehindPlayer)
		{
			jumpScare = true;
			toBehindPlayer = false;
		}
		if(jumpScare)
		{
			if((playerAngle < behindPlayerAngle + 90) && playerAngle > behindPlayerAngle)
			{				
				gameObject.transform.position = leftOfPlayer.transform.position;
				jumpScare = false;
			}
			if((playerAngle > behindPlayerAngle - 90) && (playerAngle < behindPlayerAngle))
			{				
				print ("Play Animator");
				gameObject.transform.position = rightOfPlayer.transform.position;
				jumpScare = false;
				camera.GetComponent<Animation>().Play("RotateRight");
				mouseLook.GetComponent<MouseLook>().enabled = false;
				//INVENTORY CLASS IS CAUSEING THE MOUSE TO KEEP WORKING
			}
		}
		if(run)
		{
			nav.SetDestination(escapeDestination.position);
			turnUp.speed = 10; //10
		}
		if(seek)
		{
			turnUp.speed = 1; //1
			nav.SetDestination (main.transform.position);
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
			collide.radius = 8; //8
		}
		if(toBehindPlayer)
		{
			transform.position = BehindPlayer.transform.position;
		}
		if(testingNight.isFlashLight == false && start == false && seek == true && run == false)
		{
			print ("Speed increase");
			turnUp.speed = 2;
		}
		if(testingNight.isFlashLight == true && start == false && seek == true && run == false)
		{
			turnUp.speed = 1; //1
		}
	}
}

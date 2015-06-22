using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
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
	GameObject cam;
	TestingNightVision testingNight;
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	PlayerController player;
	public SphereCollider collide;
	public float behindPlayerAngle;
	public float behindPlayerAngleZero;
	public float playerAngle;
	//public float camAngle;
	//public float newCamAngle;
	public float playerAngleReturn;
	MouseLook mouseLook;
	MouseLook playerLook;
	public bool escape;
	public bool run;
	public bool seek;
	public bool flash;
	public bool start;
	public bool left;
	public bool right;
	public bool toBehindPlayer;
	public bool jumpScare;
	public bool dontMove;
	public bool wasntLooking;
	public bool wasLooking;
	public bool notBehindMe;
	public bool behindOnly;
	public CharacterMotorC motor;
	public Rigidbody rigid;



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
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		mouseLook = (MouseLook)GameObject.Find ("Main Camera").GetComponent ("MouseLook");
		playerLook = (MouseLook)GameObject.Find ("First Person Controller").GetComponent ("MouseLook");
		motor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotorC>();
		rigid = gameObject.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main && start == true && seek == false)
		{
			StartCoroutine (StartGame());
		}
		if(other.gameObject == main && seek == true && start == false)
		{
			//print ("Start Coroutine?");
			StartCoroutine (TurnAround ());
		}

		if(other.gameObject == main && escape == true && seek == false)
		{
			StartCoroutine (HideAndSeek ());
		}
		if(other.gameObject == mother && run == false)
		{
			StartCoroutine (FoundMother ());
		}
		if(other.gameObject == main && dontMove)
		{
			StartCoroutine (WaitForLight());
		}
	}

	IEnumerator StartGame()
	{
		start = false;
		collide.radius = 1.2f;
		yield return new WaitForSeconds (startingTalk);
		seek = true;
	}

	IEnumerator TurnAround()
	{
		behindPlayerAngle = main.transform.eulerAngles.y + 180;
		if(behindPlayerAngle >= 360)
		{
			behindPlayerAngle = behindPlayerAngle - 360;
		}
		if(behindPlayerAngle < 90)
		{
			behindPlayerAngleZero = behindPlayerAngle + 360;
			print (behindPlayerAngleZero);
		}
		if(player.hGInsight && wasntLooking == false)
		{
			wasLooking = true;
			toBehindPlayer = true;
		}
		if(player.hGInsight == false && wasLooking == false && notBehindMe == false)
		{
			toBehindPlayer = true;
			wasntLooking = true;
		}
		seek = false;
		yield return null;
	}
	IEnumerator WaitForLight()
	{
		//newCamAngle = cam.transform.eulerAngles.x;
		//print (camAngle);
		yield return new WaitForSeconds (2);
		dontMove = false;
		right = false;
		left = false;
		mouseLook.GetComponent<MouseLook>().enabled = true;
		playerLook.GetComponent<MouseLook>().enabled = true;
		main.transform.rotation = Quaternion.Euler(0,playerAngleReturn,0);
		mouseLook.transform.rotation = Quaternion.Euler (0, playerAngleReturn, 0);
		rigid.useGravity = true;
		motor.canControl = true;
		cam.transform.rotation = Quaternion.Euler (0, playerAngleReturn, 0);
		gameObject.transform.position = gameObject.transform.position + new Vector3 (0,-.5f,0);
		flash = false;
		seek = false;
		run = true;
		yield return new WaitForSeconds (wait);
		escape = true;
		wasLooking = false;
		notBehindMe = false;
		wasntLooking = false;
		behindOnly = false;
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
		//camAngle = cam.transform.eulerAngles.x;
		playerAngle = main.transform.eulerAngles.y;
		if(wasntLooking)
		{
			if(((playerAngle < behindPlayerAngle + 20) && playerAngle > behindPlayerAngle - 20) && toBehindPlayer || (playerAngle > behindPlayerAngle - 20) && (playerAngle < behindPlayerAngle + 20) && toBehindPlayer)
			{
				behindPlayerAngle = main.transform.eulerAngles.y + 180;
				if(behindPlayerAngle >= 360)
				{
					behindPlayerAngle = behindPlayerAngle - 360;
				}
				notBehindMe = true;
			}
			if(((playerAngle < behindPlayerAngle + 90) && playerAngle > behindPlayerAngle) && toBehindPlayer && notBehindMe || (playerAngle > behindPlayerAngle - 90) && (playerAngle < behindPlayerAngle) && toBehindPlayer && notBehindMe)
			{
				print ("Test");
				jumpScare = true;
				toBehindPlayer = false;
			}
			if(((playerAngle > behindPlayerAngle + 270) && playerAngle < behindPlayerAngle + 360) && toBehindPlayer && notBehindMe)
			{
				print ("Test2");
				jumpScare = true;
				toBehindPlayer = false;
				behindOnly = true;
			}
			if(((playerAngle > behindPlayerAngle - 360) && playerAngle < behindPlayerAngle - 270) && toBehindPlayer && notBehindMe)
			{
				print ("Test3");
				jumpScare = true;
				toBehindPlayer = false;
				behindOnly = true;
			}

		}
		if(wasntLooking == false)
		{
			if(((playerAngle < behindPlayerAngle + 90) && playerAngle > behindPlayerAngle) && toBehindPlayer || (playerAngle > behindPlayerAngle - 90) && (playerAngle < behindPlayerAngle) && toBehindPlayer)
			{
				//print ("Test1");
				jumpScare = true;
				toBehindPlayer = false;
			}
			if(((playerAngle < behindPlayerAngleZero) && playerAngle > behindPlayerAngleZero - 90) && toBehindPlayer) /*|| (playerAngle > behindPlayerAngleZero) && (playerAngle < behindPlayerAngleZero + 90) && toBehindPlayer)*/
			{
				//print ("Test2");
				jumpScare = true;
				toBehindPlayer = false;
			}
			if(((playerAngle > behindPlayerAngle - 360) && playerAngle < behindPlayerAngle - 270) && toBehindPlayer)
			{
				//print ("Test3");
				jumpScare = true;
				toBehindPlayer = false;
				behindOnly = true;
			}
		}

		if(jumpScare)
		{
			if((playerAngle < behindPlayerAngle + 90) && playerAngle > behindPlayerAngle)
			{		
				print ("Test4");

				gameObject.transform.position = leftOfPlayer.transform.position;
				dontMove = true;
				left = true;
				jumpScare = false;
			}
			if((playerAngle < behindPlayerAngleZero) && playerAngle > behindPlayerAngleZero - 90)
			{
				print ("Test5");

				gameObject.transform.position = rightOfPlayer.transform.position;
				dontMove = true;
				right = true;
				jumpScare = false;
			}
			if((playerAngle > behindPlayerAngle - 90) && (playerAngle < behindPlayerAngle))
			{		
				print ("Test6");

				gameObject.transform.position = rightOfPlayer.transform.position;
				dontMove = true;
				right = true;
				jumpScare = false;
			}
			if((playerAngle > behindPlayerAngleZero) && playerAngle < behindPlayerAngleZero + 90 && behindOnly)
			{
				print ("Test7");

				gameObject.transform.position = leftOfPlayer.transform.position;
				dontMove = true;
				left = true;
				jumpScare = false;
			}
			if((playerAngle > behindPlayerAngleZero - 90) && playerAngle < behindPlayerAngleZero && behindOnly)
			{		
				print ("Test9");
				
				gameObject.transform.position = leftOfPlayer.transform.position;
				dontMove = true;
				left = true;
				jumpScare = false;
			}
			if((playerAngle < behindPlayerAngleZero + 360) && playerAngle > behindPlayerAngleZero + 270 && behindOnly)
			{
				print ("Test8");
				
				gameObject.transform.position = rightOfPlayer.transform.position;
				dontMove = true;
				right = true;
				jumpScare = false;
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
		if(dontMove)
		{
			seek = true;
			rigid.useGravity = false;
			gameObject.transform.position = gameObject.transform.position + new Vector3 (0,.5f,0);
			motor.canControl = false;
			mouseLook.GetComponent<MouseLook>().enabled = false;
			playerLook.GetComponent<MouseLook>().enabled = false;
			if(right)
			{
				playerAngleReturn = main.transform.eulerAngles.y + 90;
			}
			if(left)
			{
				playerAngleReturn = main.transform.eulerAngles.y - 90;
			}
		}
	}
}

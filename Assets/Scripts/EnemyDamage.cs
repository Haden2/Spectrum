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
	public float setPoint;
	public float behindPoint;
	public float rightPoint;
	public float leftPoint;
	public float playerAngle;
	public float playerAngleReturn;
	MouseLook mouseLook;
	MouseLook playerLook;
	public bool escape;
	public bool run;
	public bool seek;
	public bool flash;
	public bool start;
	public bool toBehindPlayer;
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
		leftPoint = 1000;
		rightPoint = 1000;
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main && start == true && seek == false)
		{
			StartCoroutine (StartGame());
		}
		if(other.gameObject == main && seek == true && start == false)
		{
			if(player.hGInsight == true)
			{
				wasLooking = true;
				wasntLooking = false;
			}
			if(player.hGInsight == false)
			{
				wasntLooking = true;
				wasLooking = false;
			}
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
		setPoint = main.transform.eulerAngles.y;
		leftPoint = leftOfPlayer.transform.eulerAngles.y;
		rightPoint = rightOfPlayer.transform.eulerAngles.y;

		if(wasntLooking && dontMove == false)
		{
			behindPoint = BehindPlayer.transform.eulerAngles.y;
			//notBehindMe = false;
			//Possible issue here
			behindOnly = true;
			toBehindPlayer = true;
		}
		if(wasLooking && dontMove == false)
		{
			toBehindPlayer = true;
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
		playerAngle = main.transform.eulerAngles.y;

		if(playerAngle > leftPoint - 10 && playerAngle < leftPoint + 1 && toBehindPlayer && wasLooking)
		{
		//	print("Test1");
			gameObject.transform.position = leftOfPlayer.transform.position;
			playerAngleReturn = main.transform.eulerAngles.y - 90;
			toBehindPlayer = false;
			dontMove = true;
		}
		if(playerAngle > rightPoint - 1 && playerAngle < rightPoint + 10 && toBehindPlayer && wasLooking)
		{
		//	print("Test2");
			gameObject.transform.position = rightOfPlayer.transform.position;
			playerAngleReturn = main.transform.eulerAngles.y + 90;
			toBehindPlayer = false;
			dontMove = true;
		}
		if(behindOnly)
		{
			//print ("Test3");
			if(playerAngle > behindPoint - 10 && playerAngle < behindPoint + 10)
			{
				//print ("Test4");
				notBehindMe = true;
			}
			if(playerAngle > rightPoint - 10 && playerAngle < rightPoint + 10 && toBehindPlayer && notBehindMe)
			{
				//print ("Test5");
				gameObject.transform.position = leftOfPlayer.transform.position;
				playerAngleReturn = main.transform.eulerAngles.y - 90;
				notBehindMe = false;
				toBehindPlayer = false;
				dontMove = true;
			}
			if(playerAngle > leftPoint - 10 && playerAngle < leftPoint + 10 && toBehindPlayer && notBehindMe)
			{
				//print ("Test6");
				gameObject.transform.position = rightOfPlayer.transform.position;
				playerAngleReturn = main.transform.eulerAngles.y + 90;
				notBehindMe = false;
				toBehindPlayer = false;
				dontMove = true;
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
			rightPoint = 1000;
			leftPoint = 1000;
			seek = true;
			rigid.useGravity = false;
			gameObject.transform.position = gameObject.transform.position + new Vector3 (0,.5f,0);
			motor.canControl = false;
			mouseLook.GetComponent<MouseLook>().enabled = false;
			playerLook.GetComponent<MouseLook>().enabled = false;
		}
	}
}

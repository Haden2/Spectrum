using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
	public float randomNumber;
	public float speed = 1;
	public float directionChangeInterval = 10;
	public float thatDirectionY;
	PlayerController player;
	GameObject main;
	public bool attack;
	public bool newDestination;
	NavMeshAgent wander;
	Transform zero;
	Transform one;
	Transform two;
	Transform three;
	Transform four;
	Transform five;
	Transform six;
	Transform seven;
	Transform eight;
	Transform nine;

	float heading;
	Vector3 targetRotation;
	
	void Awake ()
	{
		main = GameObject.FindGameObjectWithTag ("Player");
		wander = GetComponent<NavMeshAgent> ();
		zero = GameObject.Find ("Zero").transform;
		one = GameObject.Find ("One").transform;
		two = GameObject.Find ("Two").transform;
		three = GameObject.Find ("Three").transform;
		four = GameObject.Find ("Four").transform;
		five = GameObject.Find ("Five").transform;
		six = GameObject.Find ("Six").transform;
		seven = GameObject.Find ("Seven").transform;
		eight = GameObject.Find ("Eight").transform;
		nine = GameObject.Find ("Nine").transform;
		wander.speed = 1f;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		//wander.autoBraking=false;
		StartCoroutine(NewHeading());
	}
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main && attack == true)
		{
			StartCoroutine (NewDestination());
		}
	}
	void Update ()
	{
		if(randomNumber == 0)
		{
			wander.SetDestination(zero.transform.position);
		}
		if(randomNumber == 1)
		{
			wander.SetDestination(one.transform.position);
		}
		if(randomNumber == 2)
		{
			wander.SetDestination(two.transform.position);
		}
		if(randomNumber == 3)
		{
			wander.SetDestination(three.transform.position);
		}
		if(randomNumber == 4)
		{
			wander.SetDestination(four.transform.position);
		}
		if(randomNumber == 5)
		{
			wander.SetDestination(five.transform.position);
		}
		if(randomNumber == 6)
		{
			wander.SetDestination(six.transform.position);
		}
		if(randomNumber == 7)
		{
			wander.SetDestination(seven.transform.position);
		}
		if(randomNumber == 8)
		{
			wander.SetDestination(eight.transform.position);
		}
		if(randomNumber == 9)
		{
			wander.SetDestination(nine.transform.position);
		}
		if(attack)
		{
			player.oldManSeen = false;
			wander.acceleration = 25;
			wander.speed = 10;
			wander.SetDestination(player.transform.position);
			Vector3 endPivotDir = player.transform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, endPivotDir, 1,10);
			transform.rotation = Quaternion.LookRotation(newDir);
			zero.transform.position = new Vector3 (-1,1,20); //359
			one.transform.position = new Vector3 (-16,1,10); //315 
			two.transform.position = new Vector3 (-22,1,0); //270
			three.transform.position = new Vector3 (-12,1,-18); //225
			four.transform.position = new Vector3 (0,1,-20); //180
			five.transform.position = new Vector3 (16,1,-19); //135
			six.transform.position = new Vector3 (18,1,0); //90
			seven.transform.position = new Vector3 (22,1,22); //45
			eight.transform.position = new Vector3 (1,1,20); //1
		}
		if(newDestination)
		{
			attack = false;
			if(thatDirectionY >= .1f && thatDirectionY <=45)
			{
				randomNumber = 8;
			}
			if(thatDirectionY >= 45.1f && thatDirectionY <=90)
			{
				randomNumber = 7;
			}
			if(thatDirectionY >= 90.1f && thatDirectionY <=135)
			{
				randomNumber = 6;
			}
			if(thatDirectionY >= 135.1f && thatDirectionY <=180)
			{
				randomNumber = 5;
			}
			if(thatDirectionY >= 180.1f && thatDirectionY <=225)
			{
				randomNumber = 4;
			}
			if(thatDirectionY >= 225.1f && thatDirectionY <=270)
			{
				randomNumber = 3;
			}
			if(thatDirectionY >= 270.1f && thatDirectionY <=315)
			{
				randomNumber = 2;
			}
			if(thatDirectionY >= 315.1f && thatDirectionY <=359.9)
			{
				randomNumber = 1;
			}
		}
		if(player.oldManSeen && newDestination == false)
		{
			attack = true;
			//print ("oldMan Seen");
			randomNumber = 10;
		}
		if (wander.remainingDistance < 0.2f && attack == false)
		{
			NewHeading();
		}
		//if(wander.remainingDistance < 3 && attack == true)
		//{
		//	StartCoroutine (NewDestination());
		//}
		//print (wander.destination);
		//print (randomNumber);
	}

	IEnumerator NewHeading ()
	{
		while (true) 
		{
			yield return new WaitForSeconds(directionChangeInterval);
			NewHeadingRoutine();
		}
	}
	IEnumerator NewDestination()
	{
		thatDirectionY = transform.eulerAngles.y;
		print (thatDirectionY);
		wander.speed = 10;
		attack = false;
		newDestination = true;
		yield return null;
	}
	void NewHeadingRoutine ()
	{
		//print ("New Number");
		if(attack == false && newDestination == false)
		{
		randomNumber = Random.Range (0, 9);
		}
	}
}

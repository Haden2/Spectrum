using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	GameObject oldMan;
	GameObject hG;
	GameObject eyesHere;
	GameObject cam;
	GameObject activationSwitch;
	EnemyDamage enemyDamage;
	Wander wander;
	Inventory inventory;
	Environmental environment;
	CollectItem collect;
	public bool oldManSeen;
	public bool peripheral;
	public bool hGInsight;
	float power;
	float hGViewAngle = 135;
	float oldManViewAngle = 60;
	public float walkSpeed = 6;
	public float crouchSpeed = 3;
	public float runSpeed = 15;
	public CharacterMotorC chMotor;
	public Transform tr;
	public float dist;


	void Start()
	{
		eyesHere = GameObject.FindGameObjectWithTag ("EyesHere");
		activationSwitch = GameObject.Find ("Activation Switch");
		hG = GameObject.FindGameObjectWithTag ("Enemy");
		enemyDamage = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyDamage> ();
		wander = GameObject.FindGameObjectWithTag ("OldMan").GetComponent<Wander> ();
		inventory = GetComponent<Inventory> ();
		environment = GameObject.Find ("Environment").GetComponent<Environmental>();
		collect = GameObject.Find ("First Person Controller").GetComponent<CollectItem> ();
		oldMan = GameObject.FindGameObjectWithTag ("OldMan");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		chMotor =  GetComponent<CharacterMotorC>();
		tr = transform;
		CharacterController ch = GetComponent<CharacterController>();
		dist = ch.height/2; // calculate distance to ground
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.name == "Little Door")
		{
			environment.elevatorDoor = true;
		}
		if(environment.elevatorDoor && inventory.activeElevatorKey)
		{
			environment.canActive = true;
		}
		if(other.gameObject.name == "Elevator")
		{
			environment.inElevator = true;
		}

		if(other.gameObject == activationSwitch)
		{
			if(inventory.activeKey)
			{
				StartCoroutine (environment.WantToOpenDoor());
			}
			if(collect.keyIsGot)
			{
				environment.awaitingKey = true;
			}
		}
		if(other.gameObject == activationSwitch && environment.haveKey == false)
		{
			StartCoroutine (environment.CantOpenDoor());
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "Little Door")
		{
			environment.elevatorDoor = false;
		}
		if(other.gameObject.name == "Elevator")
		{
			environment.inElevator = false;
		}

		if(other.gameObject == activationSwitch)
		{
			StartCoroutine (environment.DoNothing());
		}
	}

	void OnParticleCollision(GameObject other)
	{
		//print (gameObject.name);
	}
	
	void Update()
	{
		//print (chMotor.movement.velocity);
		float vScale = 1.0f;
		float speed = walkSpeed;
		
		if ((Input.GetKey ("left shift") || Input.GetKey ("right shift")) && chMotor.grounded && chMotor.movement.velocity != new Vector3 (0,0,0)) 
		{
			speed = runSpeed; 
			Camera.main.fieldOfView += 40 * Time.deltaTime;
			if (Camera.main.fieldOfView > 60) {
				Camera.main.fieldOfView = 60;
			}
		} else {
			Camera.main.fieldOfView -= 40 *Time.deltaTime;
			if(Camera.main.fieldOfView <=50)
			{
				Camera.main.fieldOfView = 50;
			}
		}
		
		if (Input.GetKey("c"))
		{ // press C to crouch
			vScale = 0.5f;
			speed = crouchSpeed; // slow down when crouching
		}
		
		chMotor.movement.maxForwardSpeed = speed; // set max speed
		float ultScale = tr.localScale.y; // crouch/stand up smoothly 
		
		Vector3 tmpScale = tr.localScale;
		Vector3 tmpPosition = tr.position;
		
		tmpScale.y = Mathf.Lerp(tr.localScale.y, vScale, 5 * Time.deltaTime);
		tr.localScale = tmpScale;
		
		tmpPosition.y += dist * (tr.localScale.y - ultScale); // fix vertical position        
		tr.position = tmpPosition;

		////////////////////////////////////////////////////////////////////////////////////////////

		if(enemyDamage.dontMove)
		{
			/*Vector3 targetDir = eyesHere.transform.position - transform.position;
			float timeSpeed = speed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (cam.transform.LookAt(eyesHere.transform.position), targetDir, timeSpeed, 0.0F);
			Debug.DrawRay(cam.transform.position, newDir, Color.red); //To the Eyes.
			Debug.DrawRay(transform.position, newDir, Color.green); // The bodys position to the eyes.
			transform.rotation = Quaternion.LookRotation(newDir);*/

			//cam.transform.LookAt(eyesHere.transform.position);
				Vector3 targetPoint = new Vector3(eyesHere.transform.position.x, cam.transform.position.y, eyesHere.transform.position.z) - cam.transform.position;
				Quaternion targetRotation = Quaternion.LookRotation (targetPoint, Vector3.up);
				cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRotation, Time.deltaTime * 4.0f);
		}
		if(wander.attack || wander.newDestination)
		{
			oldManSeen = false;
		}

		{
			RaycastHit hit;
			Vector3 rayDirection = oldMan.transform.position - transform.position;
			Vector3 hGDirection = hG.transform.position - transform.position;
			Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5F, 0.5F, 0));
			if(Physics.Raycast(ray, out hit, 20))
			{
				if(hit.transform.tag == "OldMan")
				{
					oldManSeen = true;
				}
				else
				{
					oldManSeen = false;
				}
				Debug.DrawLine(transform.position, hit.point, Color.white);

				//print ("Looking at " + hit.transform.name);
			}
			if ((Vector3.Angle(rayDirection, transform.forward)) <= oldManViewAngle * 0.5f)
			{
				peripheral = true;
			}
			else
			{
				peripheral = false;
			}
			if((Vector3.Angle(rayDirection, transform.forward)) > oldManViewAngle * 0.5f)
			{
				oldManSeen = false;
			}
			if((Camera.main.transform.eulerAngles.x <= 330f && Camera.main.transform.eulerAngles.x > 60f))
			{
				peripheral = false;
			}
			if((Vector3.Angle(hGDirection, transform.forward)) <= hGViewAngle * 0.5f)
			{
				hGInsight = true;
			}
			else
			{
				hGInsight = false;
			}
		}
	}
}

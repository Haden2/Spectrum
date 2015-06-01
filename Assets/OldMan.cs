using UnityEngine;
using System.Collections;

public class OldMan : MonoBehaviour {

	PlayerController player;
	NavMeshAgent nav;
	public Transform hallway;
	public Transform playerDestination;
	public Transform oldmanLocation;
	public Transform keepMoving;
	public Vector3 previousLocation;
	float speed;
	float walkRadius;
	bool atPlayer;

	// Use this for initialization
	void Start () 
	{
		walkRadius = 10;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		playerDestination = GameObject.FindGameObjectWithTag ("Player").transform;
		hallway = GameObject.FindGameObjectWithTag ("Hallway").transform;
		nav = GetComponent<NavMeshAgent>();
		speed = 10;
		keepMoving.position = playerDestination.transform.position + transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
		//Vector3 worldDeltaPosition = playerDestination.position - transform.position;
		//randomDirection += transform.position;
		//NavMeshHit hit;
		//NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
		//Vector3 finalPosition = hit.position;
		//print (worldDeltaPosition);
		//if(player.oldManSeen)
		{
			//StartCoroutine (Charge());
			//previousLocation = playerDestination.transform.position;
			//nav.speed = 2;
			//nav.SetDestination(playerDestination.position);
		//	if(nav.remainingDistance <= .5f)
			{
				//transform.rotation = Quaternion.LookRotation(transform.position - playerDestination.position);
				//Vector3 runTo = transform.position + transform.forward * speed;
				//NavMeshHit hit;
				//NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Walkable"));
				//nav.SetDestination(hit.position);
			//	atPlayer = true;
			//	print ("here");
			}
		}

		//if(atPlayer /*&& player.oldManSeen == false*/)
		{
			//nav.SetDestination(hallway.position);
		}
	}

	//IEnumerator Charge()
	//{
		//nav.SetDestination(playerDestination.position);
		//yield return null; 
	//}
}

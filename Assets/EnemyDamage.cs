﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyDamage : MonoBehaviour 
{
	public GameObject BlueFlashlight;
	GameObject main; 
	Transform escapeDestination;
	NavMeshAgent nav;
	NavMeshAgent turnUp;
	Transform player;
	public float wait;
	public float readyOrNot;

	bool escape;
	bool run;
	bool seek;


	void Awake()
	{
		main = GameObject.FindGameObjectWithTag ("Player");
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		escapeDestination = GameObject.FindGameObjectWithTag ("Respawn").transform;
		nav = GetComponent<NavMeshAgent>();
		turnUp = GetComponent <NavMeshAgent>();
	}

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == main)
		{
			BlueFlashlight.gameObject.SetActive(false);
			nav.SetDestination (escapeDestination.position);
			seek = false;
			StartCoroutine (WaitForLight ());
		}

		if(other.gameObject == main && escape == true)
		{
			BlueFlashlight.gameObject.SetActive(true);
			//run = false;
			StartCoroutine (HideAndSeek ());
		}

	/*	if(other.gameObject == main && seek == true)
		{
			BlueFlashlight.gameObject.SetActive(false);
			nav.SetDestination (escapeDestination.position);
			seek = false;
			StartCoroutine (WaitForLight ());
		}*/
	}

	IEnumerator WaitForLight()
	{
		run = true;
		yield return new WaitForSeconds (wait);
		escape = true;
	}

	IEnumerator HideAndSeek()
	{
		yield return new WaitForSeconds (readyOrNot);
		seek = true;
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
			run = false;
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesMovementDamage : MonoBehaviour {

	public int damage;
	public float movement;
	private bool roomHasMonsters;
	private GameObject closest;

	// Use this for initialization
	void Start () {
		roomHasMonsters = true;
		closest = null;
		FindClosestMonster();
	}

	public GameObject FindClosestMonster()
	{
		GameObject[] monstersInRoom;
		monstersInRoom = GameObject.FindGameObjectsWithTag("Monster");
		float distance = Mathf.Infinity;
		Vector3 heroPosition = transform.position;
		foreach (GameObject closetMonster in monstersInRoom)
		{
			Vector3 diff = closetMonster.transform.position - heroPosition;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closest = closetMonster;
				distance = curDistance;
			}
		}
		return closest;
	}

	// Update is called once per frame
	void Update () {
		if (roomHasMonsters == true) {
			transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, movement);
			transform.LookAt(closest.transform);
			FindClosestMonster ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Monster") {
			movement = 0;
			InvokeRepeating ("closest.TakingDamage(damage)", 1f, 5f);
		}
	}
}
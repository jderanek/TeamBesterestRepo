﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		this.AssignStats (20, 2, 25, 1, 100, 2);
	}

	//Overrides base Attack function to heal a hero in the room instead of attacking
	public override void Attack(RoomScript room) {
		BaseHero heroScript;
		BaseHero mostHurt = null;
		float hpRatio = 100000f;
		float curRatio;
		int threat = -1;
		foreach (GameObject hero in room.heroesInRoom) {
			heroScript = hero.GetComponent<BaseHero> ();

			if (heroScript != null && heroScript != this) {
				curRatio = ((float)heroScript.getHealth () / heroScript.getMaxHealth ());
				if (curRatio < hpRatio)
					mostHurt = heroScript;
			}
		}

		//Heals the most hurt hero, or calls the parent Attack function to prevent
		//This unit from healing itself
		if (mostHurt != null)
			mostHurt.Heal (this.getDamage ());
		else
			base.Attack (room);
	}
}

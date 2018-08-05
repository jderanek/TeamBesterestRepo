using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderHero : BaseHero {

	bool canStun = true;

	//Assigns all initial stats
	void Start () {
		//this.AssignStats (30, 3, 20, 3, 200, 6);
		this.AssignStats("Crusader");
	}

	//Refreshes the canStun bool
	public override void MoveTo (BaseRoom room) {
		base.MoveTo (room);
		canStun = true;
	}

	//Overrides the attack function to stun if possible
	public override void Attack () {
		BaseMonster monScript;
		BaseMonster highThreat = null;
		int threat = -1;
		foreach (GameObject mon in this.getRoom().roomMembers) {
			if (mon == null)
				continue;

			monScript = mon.GetComponent<BaseMonster>();

			if (monScript != null) {
				if (monScript.getThreat() > threat) {
					highThreat = monScript;
					threat = monScript.getThreat();
				}
			}

		}

		//Makes mosnter take damage
		if (highThreat != null) {
			if (canStun && highThreat.getArmor () < 2) {
				canStun = false;
				highThreat.TakeDamage (this.getDamage () + 2);
				highThreat.Stun ();
			} else
				highThreat.TakeDamage (this.getDamage ());
		}
	}
}

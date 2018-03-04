using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		this.AssignStats (12, 6, 15, 0, 200, 4);
	}

	//Placeholder to be replaced by mage multiattack
	public override void Attack(RoomScript room) {
		MonsterScript monScript;
		MonsterScript highThreat = null;
		int threat = -1;
		foreach (GameObject mon in room.roomMembers) {
			monScript = mon.GetComponent<MonsterScript> ();

			if (monScript != null) {
				if (monScript.curThreat > threat) {
					highThreat = monScript;
					threat = monScript.curThreat;
				}
			}
		}

		//Makes mosnter take damage
		if (highThreat != null)
			highThreat.TakeDamage(this.getDamage());
	}
}

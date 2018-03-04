using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueHero : BaseHero {

	//Assigns all initial stats
	void Start () {
		this.AssignStats (15, 4, 15, 1, 300, 2);
	}

	//Searches the list of monsters and attacks the monster with the highest threat
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
}
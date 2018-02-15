using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyTrait : TraitBase {

	//Decreases infamy gain
	public override void ApplyBase(MonsterScript monster) {
		monster.infamyGain -= 1;
		//No idea what this should be either
	}

	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
	}

	//Reduces all other monsters morale by 5
	public override void ApplyWeekEffects(MonsterScript monster) {
		RoomScript room = monster.myRoom.GetComponent<RoomScript> ();

		foreach (GameObject mon in room.roomMembers) {
			MonsterScript monScript = mon.GetComponent<MonsterScript> ();
			if (monScript != null && monScript != monster) {
				if (monScript.trait1 == "Gross" || monScript.trait2 == "Gross") {
					monster.morale = Mathf.Clamp01 (monster.morale - .05f);
				} else {
					monster.morale = Mathf.Clamp01 (monster.morale + .05f);
				}
			}
		}
	}
}

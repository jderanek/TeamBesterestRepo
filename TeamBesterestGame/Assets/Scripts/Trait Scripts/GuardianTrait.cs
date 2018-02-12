using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTrait : TraitBase {


	//Empty function
	public override void ApplyWeekEffects(MonsterScript monster) {
	}

	//Adds nevrve and adjusts threat value
	//Then checks for smaller monsters to change morale
	public override void ApplyDayEffects(MonsterScript monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve + .1f);
		monster.curThreat = monster.threatValue + 1;

		RoomScript room = monster.myRoom.GetComponent<RoomScript> ();

		foreach (GameObject mon in room.roomMembers) {
			MonsterScript monScript = mon.GetComponent<MonsterScript> ();
			if (monScript != null && monScript.size > monster.size) {
				monster.morale = Mathf.Clamp01 (monster.morale + .15f);
				return;
			}
		}

		monster.morale = Mathf.Clamp01 (monster.morale - .1f);
	}
}

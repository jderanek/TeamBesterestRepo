using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlackerTrait : TraitBase {

	//Increases stress gain by 2
	public override void ApplyBase(MonsterScript monster) {
		monster.stressGain += .02f;
		monster.vacationStressLoss = .2f;
	}

	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
	}


	//Increases morale by 10 with Wprkaholic monsters.
	public override void ApplyWeekEffects(MonsterScript monster) {
		RoomScript room = monster.myRoom.GetComponent<RoomScript> ();

		foreach (GameObject mon in room.roomMembers) {
			MonsterScript monScript = mon.GetComponent<MonsterScript> ();
			if (monScript != null && monScript.personality.GetType() == typeof(WorkaholicTrait)) {
				monster.morale = Mathf.Clamp01 (monster.morale + .1f);
			}
		}
	}
}
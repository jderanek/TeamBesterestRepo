using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaholicTrait : TraitBase {

	//Reduces stress gain by 1
	public override void ApplyBase(MonsterScript monster) {
		monster.stressGain -= .01f;
	}

	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
	}


	//Reduces morale by 10 with Slacker monsters.
	public override void ApplyWeekEffects(MonsterScript monster) {
		RoomScript room = monster.myRoom.GetComponent<RoomScript> ();

		foreach (GameObject mon in room.roomMembers) {
			MonsterScript monScript = mon.GetComponent<MonsterScript> ();
			if (monScript != null && monScript.personality.GetType() == typeof(SlackerTrait)) {
				monster.morale = Mathf.Clamp01 (monster.morale - .1f);
			}
		}
	}
}


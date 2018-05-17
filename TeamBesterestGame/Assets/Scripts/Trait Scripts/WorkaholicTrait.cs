using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaholicTrait : TraitBase {

	//Reduces stress gain by 1
	public override void ApplyBase(BaseMonster monster) {
		monster.setStressGain(monster.getStressGain() - .01f);
		this.setName ("Workaholic");
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
	}


	//Reduces morale by 10 with Slacker monsters.
	public override void ApplyWeekEffects(BaseMonster monster) {
		RoomScript room = monster.getCurRoom();

		foreach (GameObject mon in room.roomMembers) {
			BaseMonster monScript = mon.GetComponent<BaseMonster> ();
			if (monScript != null && monScript.getTrait().GetType() == typeof(SlackerTrait)) {
				monster.setMorale(Mathf.Clamp01 (monster.getMorale() - .1f));
			}
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlackerTrait : TraitBase {

	//Increases stress gain by 2
	public override void ApplyBase(BaseMonster monster) {
		monster.setStressGain (monster.getStressGain () + .02f);
		monster.setStressLoss (2f);
		this.setName ("Slacker");
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
	}


	//Increases morale by 10 with Wprkaholic monsters.
	public override void ApplyWeekEffects(BaseMonster monster) {
		RoomScript room = monster.getCurRoom ();

		foreach (GameObject mon in room.roomMembers) {
			BaseMonster monScript = mon.GetComponent<BaseMonster> ();
			if (monScript != null && monScript.getTrait().GetType() == typeof(WorkaholicTrait)) {
				monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .1f));
			}
		}
	}
}
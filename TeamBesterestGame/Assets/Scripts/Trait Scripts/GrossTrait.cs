using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrossTrait : TraitBase {

	//Increases infamy gain
	public override void ApplyBase(BaseMonster monster) {
		monster.setInfamyGain (monster.getInfamyGain () + 1);
		this.setName ("Gross");
		//No idea what this should be either
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
	}

	//Reduces all other monsters morale by 5
	public override void ApplyWeekEffects(BaseMonster monster) {
		RoomScript room = monster.getCurRoom ();

		foreach (GameObject mon in room.roomMembers) {
			BaseMonster monScript = mon.GetComponent<BaseMonster> ();
			if (monScript != null && monScript != monster) {
				monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .05f));
			}
		}
	}
}

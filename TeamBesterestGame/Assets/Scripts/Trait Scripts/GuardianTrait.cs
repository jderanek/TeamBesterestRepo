using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianTrait : TraitBase {

	//Adds threat to this monster
	public override void ApplyBase(BaseMonster monster) {
		monster.addThreat (1);
		this.setName ("Guardian");
	}


	//Empty function
	public override void ApplyWeekEffects(BaseMonster monster) {
	}

	//Adds nevrve and adjusts threat value
	//Then checks for smaller monsters to change morale
	public override void ApplyDayEffects(BaseMonster monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve + .1f);

		RoomScript room = monster.getCurRoom ();

		foreach (GameObject mon in room.roomMembers) {
			BaseMonster monScript = mon.GetComponent<BaseMonster> ();
			if (monScript != null && monScript.getSize() > monster.getSize()) {
				monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .15f));
				return;
			}
		}

		monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .1f));
	}
}

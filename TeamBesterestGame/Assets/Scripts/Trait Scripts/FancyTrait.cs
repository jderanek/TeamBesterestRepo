using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyTrait : TraitBase {

	//Decreases infamy gain
	public override void ApplyBase(BaseMonster monster) {
		monster.setInfamyGain (monster.getInfamyGain () - 1);
		this.setName ("Fancy");
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
				//if (monScript.trait1 == "Gross" || monScript.trait2 == "Gross") {
				if (monScript.getTrait().getName() == "Gross") {
					monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .05f));
				} else {
					monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .05f));
				}
			}
		}
	}
}

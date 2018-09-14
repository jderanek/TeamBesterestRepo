using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardlyTrait : TraitBase {

	//Empty function
	public override void ApplyBase(BaseMonster monster) {
		this.setName ("Cowardly");
	}

	//Gains morale if room threat is high, loses it if it is low
	public override void ApplyDayEffects(BaseMonster monster) {
		int threat = monster.getCurRoom ().roomThreat;

		switch (threat) {
		case 1:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .50f));
			break;
		case 2:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .25f));
			break;
		case 3:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .10f));
			break;
		case 4:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale ()));
			break;
		case 5:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .10f));
			break;
		case 6:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .25f));
			break;
		default:
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .50f));
			break;
		}
	}

	//Empty function
	public override void ApplyWeekEffects(BaseMonster monster) {
		return;
	}
}

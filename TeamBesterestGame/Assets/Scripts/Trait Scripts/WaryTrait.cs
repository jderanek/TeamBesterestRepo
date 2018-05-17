using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaryTrait : TraitBase {

	//Removes threat from this monster
	public override void ApplyBase(BaseMonster monster) {
		monster.addThreat(-1);
		this.setName ("Wary");
	}

	//Reduces nerve and threat level
	public override void ApplyDayEffects(BaseMonster monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve - .25f);
		//monster.curThreat = monster.threatValue - 1;
	}

	//Increases morale and removes stress if mosnter didn't fight
	public override void ApplyWeekEffects(BaseMonster monster) {
		if (!monster.getHasFought()) {
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () + .2f));
			monster.setStress (Mathf.Clamp01 (monster.getStress () - .02f));
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessTrait : TraitBase {

	//Adds 25% damage increase
	public override void ApplyBase(BaseMonster monster) {
		monster.setCurDamage ((int)(monster.getBaseDamage () * 1.25));
		this.setName ("Reckless");
	}

	//Adds nerve to the monster at the start of the day
	public override void ApplyDayEffects(BaseMonster monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve + .25f);
	}

	//Adds morale to monster if hasFought is true
	public override void ApplyWeekEffects(BaseMonster monster) {
		if (!monster.getHasFought()) {
			monster.setMorale (Mathf.Clamp01 (monster.getMorale () - .2f));
			monster.setStress (Mathf.Clamp01 (monster.getStress () + .04f));
		}
	}
}

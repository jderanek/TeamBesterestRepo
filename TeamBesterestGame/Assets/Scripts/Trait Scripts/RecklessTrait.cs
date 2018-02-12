using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessTrait : TraitBase {

	// Use this for initialization
	void Start () {
	}
	
	//Adds nerve to the monster at the start of the day
	public override void ApplyDayEffects(MonsterScript monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve + .25f);
	}

	//Adds morale to monster if hasFought is true
	public override void ApplyWeekEffects(MonsterScript monster) {
		if (!monster.hasFought) {
			monster.morale = Mathf.Clamp01 (monster.morale - .2f);
			monster.stress = Mathf.Clamp01 (monster.stress + .04f);
		}
	}
}

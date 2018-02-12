using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessTrait : TraitBase {

	// Use this for initialization
	void Start () {
		this.setMorale (25);
	}
	
	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
		return;
	}

	//Adds morale to monster if hasFought is true
	public override void ApplyWeekEffects(MonsterScript monster) {
		if (!monster.hasFought) {
			monster.morale = Mathf.Clamp (monster.morale - .2f, 0, 100);
			monster.stress = Mathf.Clamp (monster.stress + 4, 0, 100);
		}
	}
}

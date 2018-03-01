using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaryTrait : TraitBase {

	//Removes threat from this monster
	public override void ApplyBase(MonsterScript monster) {
		monster.threatValue -= 1;
		this.setName ("Wary");
	}

	//Reduces nerve and threat level
	public override void ApplyDayEffects(MonsterScript monster) {
		monster.curNerve = Mathf.Clamp01 (monster.baseNerve - .25f);
		monster.curThreat = monster.threatValue - 1;
	}

	//Increases morale and removes stress if mosnter didn't fight
	public override void ApplyWeekEffects(MonsterScript monster) {
		if (!monster.hasFought) {
			monster.morale = Mathf.Clamp01 (monster.morale + .2f);
			monster.stress = Mathf.Clamp01 (monster.stress - .02f);
		}
	}
}

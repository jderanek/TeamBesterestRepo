using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Needs some kind of onkilling hero function
public class PridefulTrait : TraitBase {

	//Increases damage by 10%
	public override void ApplyBase(BaseMonster monster) {
		monster.addDamage ((int)(monster.getBaseDamage () * .1));
		this.setName ("Prideful");
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
	}


	//Empty function
	public override void ApplyWeekEffects(BaseMonster monster) {
	}
}

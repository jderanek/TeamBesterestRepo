using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Needs some kind of onkilling hero function
public class PridefulTrait : TraitBase {

	//Increases damage by 10%
	public override void ApplyBase(MonsterScript monster) {
		monster.attackDamage += (int)(monster.attackDamage * .1);
		this.setName ("Prideful");
	}

	//Empty function
	public override void ApplyDayEffects(MonsterScript monster) {
	}


	//Empty function
	public override void ApplyWeekEffects(MonsterScript monster) {
	}
}

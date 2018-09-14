using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlirtyTrait : TraitBase {

	//Empty Function
	public override void ApplyBase(BaseMonster monster) {
		this.setName ("Flirty");
	}

	//Empty function
	public override void ApplyDayEffects(BaseMonster monster) {
		//Can't really implement yet
	}


	//Empty function
	public override void ApplyWeekEffects(BaseMonster monster) {
	}
}

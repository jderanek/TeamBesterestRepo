using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes monster immune to bad work ethic debuffs
public class Studious : BaseEthic {

	public override void ApplyEthic(BaseMonster monster) {
		monster.canBeDebuffed = false;
	}
}


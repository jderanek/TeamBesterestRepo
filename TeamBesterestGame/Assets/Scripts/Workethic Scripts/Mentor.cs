using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases XP gain for all monsters in the room
public class Mentor : BaseEthic {

	float xpMod = 1.15f;
	BaseEthic.Target xpRec = BaseEthic.Target.AllStack;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, xpRec);

		foreach(BaseMonster mon in targets)
			mon.setXPMod (mon.getXPMod () * xpMod);
	}
}

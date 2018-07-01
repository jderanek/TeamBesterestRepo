using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gives this monster an additional Nerve Breakdown
public class Compliant : BaseEthic {

	int breaks = 1;
	BaseEthic.Target nerveRec = BaseEthic.Target.Self;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, nerveRec);

		foreach (BaseMonster mon in targets)
			mon.setBreakdowns (mon.getBreakdowns () + breaks);
	}
}

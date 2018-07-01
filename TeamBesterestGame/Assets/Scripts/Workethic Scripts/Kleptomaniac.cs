using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Monster has a lower amount of gold per kill
public class Kleptomaniac : BaseEthic {

	float goldMod = .85f;
	BaseEthic.Target goldRec = BaseEthic.Target.Self;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, goldRec);

		foreach (BaseMonster mon in targets)
			mon.setGoldMod (mon.getGoldMod () + goldMod);
	}
}


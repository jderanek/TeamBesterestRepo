using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decreases other monsters morale loss, and stress gain
public class TeamPlayer : BaseEthic {

	float moraleLossMod = .9f;
	BaseEthic.Target moraleRec = BaseEthic.Target.Others;
	float stressGainMod = .9f;
	BaseEthic.Target stressRec = BaseEthic.Target.Others;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, moraleRec);

		foreach(BaseMonster mon in targets)
			mon.setMoraleLoss (mon.getMoraleLoss() * moraleLossMod);

		targets = BaseEthic.GetTargets (monster, stressRec);

		foreach (BaseMonster mon in targets)
			mon.setStressGain (mon.getStressGain () * stressGainMod);
	}
}

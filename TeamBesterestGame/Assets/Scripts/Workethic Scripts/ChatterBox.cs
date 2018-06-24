using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increase other monsters stress gain
public class ChatterBox : BaseEthic {

	BaseEthic.Target stressRec = BaseEthic.Target.Others;
	float stressGainMod = 1.25f;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, stressRec);

		foreach (BaseMonster mon in targets)
			mon.setMoraleGain (mon.getMoraleGain () * stressGainMod);
	}
}

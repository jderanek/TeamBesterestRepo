using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases monsters damage, and lowers morale loss
public class Focused : BaseEthic {

	float damMod = 1.10f;
	BaseEthic.Target damRec = BaseEthic.Target.Self;
	float moraleLossMod = .75f;
	BaseEthic.Target moraleRec = BaseEthic.Target.Self;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, damRec);

		foreach(BaseMonster mon in targets)
			mon.setCurDamage ((int) Mathf.Round(mon.getCurDamage () * damMod));

		targets = BaseEthic.GetTargets (monster, moraleRec);

		foreach (BaseMonster mon in targets)
			mon.setMoraleLoss (mon.getMoraleLoss () * moraleLossMod);
	}
}

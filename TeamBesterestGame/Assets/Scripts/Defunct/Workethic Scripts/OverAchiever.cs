using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases monsters damage, and other monsters morale increase
public class OverAchiever : BaseEthic {

	float damMod = 1.15f;
	BaseEthic.Target damRec = BaseEthic.Target.Self;
	float moraleGainMod = 1.15f;
	BaseEthic.Target moraleRec = BaseEthic.Target.Others;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, damRec);

		foreach(BaseMonster mon in targets)
			mon.setCurDamage ((int) Mathf.Round(mon.getCurDamage () * damMod));
		
		targets = BaseEthic.GetTargets (monster, moraleRec);

		foreach (BaseMonster mon in targets)
			mon.setMoraleGain (mon.getMoraleGain () * moraleGainMod);
	}
}

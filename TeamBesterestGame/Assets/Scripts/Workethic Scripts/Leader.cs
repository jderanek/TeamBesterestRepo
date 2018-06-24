using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases damage of all room members once
public class Leader : BaseEthic {

	float damMod = 1.15f;
	BaseEthic.Target damRec = BaseEthic.Target.AllNoStack;
	string tag = "Leader";

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, damRec, tag);

		foreach(BaseMonster mon in targets)
			mon.setCurDamage ((int) Mathf.Round(mon.getCurDamage () * damMod));
	}
}

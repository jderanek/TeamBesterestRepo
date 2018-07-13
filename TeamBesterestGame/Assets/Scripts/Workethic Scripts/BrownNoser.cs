using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Increases damage and XP gain when other monsters are in the room.
public class BrownNoser : BaseEthic {

	float damMod = 1.10f;
	BaseEthic.Target damRec = BaseEthic.Target.Self;
	float xpMod = 1.10f;
	BaseEthic.Target xpRec = BaseEthic.Target.Self;

	//Checks whether or not buff should be applied
	private bool ApplyBuff(BaseMonster monster) {
		foreach (BaseMonster mon in monster.getCurRoom().roomMembers) {
			if (mon.getThreat () > monster.getThreat ())
				return true;
		}
		return false;
	}

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, damRec);

		if (ApplyBuff(monster)) {
			foreach (BaseMonster mon in targets)
				mon.setCurDamage ((int)Mathf.Round (mon.getCurDamage () * damMod));

			targets = BaseEthic.GetTargets (monster, xpRec);

			foreach (BaseMonster mon in targets)
				mon.setXPMod (mon.getXPMod () = xpMod);
		}
	}
}
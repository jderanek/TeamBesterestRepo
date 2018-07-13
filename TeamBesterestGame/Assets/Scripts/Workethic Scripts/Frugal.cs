using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes all monsters promotions a bit cheaper
public class Frugal : BaseEthic {

	BaseEthic.Target promotionRec = BaseEthic.Target.AllStack;
	float promotionMod = 1.1f;

	public override void ApplyEthic(BaseMonster monster) {
		List<BaseMonster> targets = BaseEthic.GetTargets (monster, promotionRec);

		foreach (BaseMonster mon in targets) {
			if (mon.canBeDebuffed)
				mon.setPromotionMod (mon.getPromotionMod () * promotionMod);
		}
	}
}

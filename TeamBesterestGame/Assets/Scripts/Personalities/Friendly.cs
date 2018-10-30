using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Buffs all other monsters in the room
public class Friendly : BaseTrait {
    public override void OnPhaseStart(BaseMonster monster)
    {
        BaseMonster monScript;
        foreach (GameObject ally in monster.getCurRoom().roomMembers)
        {
            monScript = ally.GetComponent<BaseMonster>();

            if (monScript != monster)
            {
                monScript.setCurDamage((int)(monScript.getBaseDamage() * 1.25f));
                monScript.SetSpeed((int)(monScript.GetBaseSpeed() * 1.25f));
            }
        }
    }

    public override void OnDeath(BaseMonster monster)
    {
        BaseMonster monScript;
        foreach (GameObject ally in monster.getCurRoom().roomMembers)
        {
            if (!ally.activeSelf)
                continue;

            monScript = ally.GetComponent<BaseMonster>();

            if (monScript != monster)
            {
                monScript.setCurDamage(monScript.getBaseDamage());
                monScript.SetSpeed(monScript.GetBaseSpeed());
            }
        }
    }
}

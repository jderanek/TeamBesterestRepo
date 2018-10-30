using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Doubles attack and speed when alone, otherwise halves it
public class Loner : BaseTrait
{
    public override void OnCombatStart(BaseMonster monster)
    {
        if (monster.getCurRoom().roomMembers.Count > 1)
        {
            monster.setCurDamage((int)(monster.getCurDamage() * .5f));
            monster.SetSpeed((int)(monster.GetSpeed() * .5f));
        } else
        {
            monster.setCurDamage((int)(monster.getCurDamage() * 2f));
            monster.SetSpeed((int)(monster.GetSpeed() * 2f));
        }
    }
}
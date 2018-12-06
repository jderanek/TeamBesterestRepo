using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Makes Coward run (die) when attacked
public class Coward : BaseTrait {

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        attacked.curHealth = 0;
        attacked.StartFleeing();
        return 0;
    }
}
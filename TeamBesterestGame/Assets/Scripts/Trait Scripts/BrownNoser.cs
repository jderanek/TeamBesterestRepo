using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownNoser : BaseTrait
{
    public BrownNoser(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        if (attacked.getCurRoom().roomMembers.Count > 1)
        {
            BaseMonster monScript;
            foreach (GameObject monObject in attacked.getCurRoom().roomMembers)
            {
                monScript = monObject.GetComponent<BaseMonster>();
                if (monScript != attacked)
                {
                    monScript.TakeDamage(dmg);
                    return 0;
                }
            }
        }
        return dmg;
    }
}
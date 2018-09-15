using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestSchoolDropout : BaseTrait
{
    public PriestSchoolDropout() : base() { }
    public PriestSchoolDropout(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        if (attacked.GetType() == System.Type.GetType("ClericHero"))
            return dmg * 3;
        return dmg;
    }
}
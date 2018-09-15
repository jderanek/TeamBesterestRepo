using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reckless : BaseTrait
{
    public Reckless() : base() { }
    public Reckless(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        if (Random.value > .5f)
            return dmg * 3;
        else
            return 0;
    }
}
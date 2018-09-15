using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichKid : BaseTrait
{
    public RichKid() : base() { }
    public RichKid(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        return dmg * (1 + (attacked.getRoom().currentGold/100));
    }
}
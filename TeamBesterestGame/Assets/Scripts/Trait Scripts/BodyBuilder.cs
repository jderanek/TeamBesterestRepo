using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBuilder : BaseTrait
{
    public BodyBuilder() : base() { this.traitName = "Body Builder"; }
    public BodyBuilder(List<PersonalityTags.Tag> tags) : base(tags) { this.traitName = "Body Builder"; }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        return dmg * 5;
    }

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        return dmg * 5;
    }
}
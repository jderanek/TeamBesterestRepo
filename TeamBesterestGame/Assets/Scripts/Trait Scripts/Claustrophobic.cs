using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claustrophobic : BaseTrait
{
    public Claustrophobic() : base() { this.traitName = "Claustrophobic"; }
    public Claustrophobic(List<PersonalityTags.Tag> tags) : base(tags) { this.traitName = "Claustrophobic"; }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        return dmg * (1 + attacked.getCurRoom().heroesInRoom.Count);
    }
}
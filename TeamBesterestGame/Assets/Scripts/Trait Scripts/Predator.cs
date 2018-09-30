using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : BaseTrait
{
    public Predator() : base() { this.traitName = "Predator"; }
    public Predator(List<PersonalityTags.Tag> tags) : base(tags) { this.traitName = "Predator"; }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null)
    {
        if (attacked.getRoom().roomMembers.Count == 1 && attacked.getRoom().heroesInRoom.Count == 1 && Random.value > .5)
            attacked.Death(attacker);

        return dmg;
    }
}
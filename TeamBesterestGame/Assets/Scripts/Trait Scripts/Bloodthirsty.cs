using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodthirsty : BaseTrait
{
    public Bloodthirsty(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override void OnHeroDeath(BaseHero dead) { }

    public override void OnHeroDamaged(int dmg, BaseHero hero, BaseMonster monster)
    {
        monster.Heal((int)dmg / 2);
    }
}

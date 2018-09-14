using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrait {
    List<PersonalityTags.Tag> tags = new List<PersonalityTags.Tag>();

    public BaseTrait(List<PersonalityTags.Tag> tags)
    {
        this.tags.AddRange(tags);
    }

    public abstract void OnDeath(BaseMonster monster);

    public abstract void OnAttack(BaseHero attacked);

    public virtual int OnAttacked(int dmg, BaseHero attacker = null)
    {
        return dmg;
    }

    public abstract void OnSpawn();

    public abstract void OnKill(BaseHero killed = null);

    public abstract void OnHeroDeath(BaseHero dead = null);

    public virtual void OnCombatEnd(BaseMonster monster) {}
}

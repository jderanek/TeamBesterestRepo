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

    public virtual int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null) {
        return dmg;
    }

    public virtual int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        return dmg;
    }

    public abstract void OnSpawn();

    public abstract void OnKill(BaseHero killed = null);

    public abstract void OnHeroDeath(BaseHero dead = null);

    public virtual void OnCombatEnd(BaseMonster monster) {}

    public virtual void OnTimePass(BaseMonster monster) { }

    public virtual void OnHeroDamaged(int dmg, BaseHero hero, BaseMonster monster) { }
}

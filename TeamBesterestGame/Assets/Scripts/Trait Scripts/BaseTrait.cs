using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrait {
    List<PersonalityTags.Tag> tags = new List<PersonalityTags.Tag>();
    public string traitName;

    public BaseTrait() { }

    public BaseTrait(List<PersonalityTags.Tag> tags)
    {
        this.tags.AddRange(tags);
    }

    public virtual void OnDeath(BaseMonster monster) { }

    public virtual int OnAttack(int dmg, BaseHero attacked, BaseMonster attacker = null) {
        return dmg;
    }

    public virtual int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker = null)
    {
        return dmg;
    }

    public virtual void OnSpawn() { }

    public virtual void OnKill(BaseHero killed = null) { }

    public virtual void OnHeroDeath(BaseHero dead = null) { }

    public virtual void OnCombatEnd(BaseMonster monster) { }

    public virtual void OnTimePass(BaseMonster monster) { }

    public virtual void OnHeroDamaged(int dmg, BaseHero hero, BaseMonster monster) { }

    public virtual void OnPhaseStart(BaseMonster monster) { }

    public virtual void OnCombatStart(BaseMonster monster) { }

    public List<PersonalityTags.Tag> GetTags()
    {
        return this.tags;
    }
}

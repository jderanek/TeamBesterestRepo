using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes half damage
public class ToughSkinned : BaseTrait
{
    public ToughSkinned() : base() { }
    public ToughSkinned(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override int OnAttacked(int dmg, BaseMonster attacked, BaseHero attacker)
    {
        return (int) (dmg/2);
    }

    public override void OnHeroDeath(BaseHero dead) { }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Takes half damage
public class ToughSkinned : BaseTrait
{
    public ToughSkinned(List<PersonalityTags.Tag> tags) : base(tags) { }

    public override void OnDeath(BaseMonster monster) { }

    public override void OnAttack(BaseHero attacked) { }

    public override void OnSpawn() { }

    public override void OnKill(BaseHero killed) { }

    public override int OnAttacked(int dmg, BaseHero attacker)
    {
        return (int) (dmg/2);
    }

    public override void OnHeroDeath(BaseHero dead) { }
}
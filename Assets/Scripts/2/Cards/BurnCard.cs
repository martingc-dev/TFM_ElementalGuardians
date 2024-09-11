using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/New Card/BurnCard")]
public class BurnCard : Cards
{
    public int burnDamage;
    public int turns;

    public override void Play(PlayerEnergy player, List<Enemy2> enemies)
    {
        base.Play(player, enemies);

        foreach (Enemy2 enemy in enemies)
        {
            enemy.ApplyBurn(burnDamage, turns);
        }

        Debug.Log("Burn applied: " + burnDamage + " damage for " + turns + " turns.");
    }
}

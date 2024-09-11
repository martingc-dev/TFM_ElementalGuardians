using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/New Card/AreaAttack")]
public class AreaAttackCard : Cards
{
    public int damageAmount;

    public override void Play(PlayerEnergy player, List<Enemy2> enemies)
    {
        base.Play(player, enemies);

        foreach (Enemy2 enemy in enemies)
        {
            enemy.TakeDamage(damageAmount);
        }

        Debug.Log("Area attack dealt " + damageAmount + " damage to all enemies.");
    }
}

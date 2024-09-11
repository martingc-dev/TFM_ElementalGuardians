using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEnemy : Enemy2
{
    //Cantidad de vida que se cura
    public int healAmount = 5;


    public override void AttackPlayer(PlayerEnergy player)
    {
        Heal();
    }

    private void Heal()
    {
        health += healAmount;
        Debug.Log(enemyName + " heals for " + healAmount + " HP. Current health: " + health);
    }

}

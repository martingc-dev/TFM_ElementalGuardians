using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   //Lista de efectos de estado aplicado al enemigo
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    //Salud del enemigo
    public int health = 50;
    // Daño de ataque del enemigo
    public int attackDamage = 10;


    /// <summary>
    /// Metodo para aplicar un nuevo efecto de estado al enemigo
    /// </summary>
    /// <param name="effect"></param>
    public void ApplyStatusEffect(StatusEffect effect)
    {
        //Agrega el efecto a la lista de efectos
        statusEffects.Add(effect);
        Debug.Log($"Aplicado {effect.effectType} con valor {effect.value} durante {effect.duration} turnos.");
    }

    /// <summary>
    /// Método que procesa todos los efectos activos al inicio de cada turno
    /// </summary>
    public void ProcessStatusEffects()
    {
        for(int i = statusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = statusEffects[i];

            if (effect.IsActive())
            {
                // Aplicar la lógica del efecto en base a su tipo
                ApplyEffect(effect);

                // Reducir la duración del efecto
                effect.ReduceDuration();
            }

            // Si la duración del efecto ha llegado a 0, eliminarlo
            if (!effect.IsActive())
            {
                statusEffects.RemoveAt(i);
                Debug.Log($"{effect.effectType} ha terminado.");
            }
        }
    }

    /// <summary>
    ///  Método para aplicar el efecto según el tipo
    /// </summary>
    /// <param name="effect"></param>
    private void ApplyEffect(StatusEffect effect)
    {
        switch(effect.effectType)
        {
            case StatusEffect.EffectType.Burn:
                int burnDamage = effect.value;
                Debug.Log($"{gameObject.name} recibe {burnDamage} de daño por quemadura.");
                ReceiveDamage(burnDamage);  // Método que aplicaría el daño
                break;
        }
    }

    /// <summary>
    /// Método para aplicar daño al enemigo
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} recibe {damage} de daño. Salud restante: {health}");

        // Lógica para eliminar al enemigo si la salud llega a 0
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha sido derrotado.");
        // Lógica para eliminar al enemigo, como desactivarlo o destruir el GameObject
        Destroy(gameObject);
    }


    public void AttackPlayer(Character player)
    {
        if (player == null)
        {
            Debug.LogError("Player is null in AttackPlayer method.");
            return;
        }
        Debug.Log($"{gameObject.name} ataca al jugador y hace {attackDamage} de daño.");
        player.ReceiveDamage(attackDamage);

        // Ejemplo de quemadura con 5 de daño y duración de 3 turnos
        StatusEffect burnEffect = new StatusEffect(StatusEffect.EffectType.Burn, 5, 3);
        player.ApplyStatusEffect(burnEffect);
    }
}

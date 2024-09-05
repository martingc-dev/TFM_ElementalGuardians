using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Lista de efectos de estao apllicado a este personaje (jugadro o enemigo)
    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    /// <summary>
    /// metodo para aplicar un nuevo efecto de estado
    /// </summary>
    /// <param name="statusEffect"></param>
    public void ApplyStatusEffect(StatusEffect effect)
    {
        //Agrega el efecto a la lista de efectos
        statusEffects.Add(effect);
         Debug.Log($"Aplicado {effect.effectType} con valor {effect.value} durante {effect.duration} turnos.");
    }

    /// <summary>
    /// Metodo que procesa todos los efectos activos al inicio de cada turno
    /// </summary>
    public void ProcessStatusEffects()
    {
        // Iterar sobre cada efecto de estado
        for (int i = statusEffects.Count - 1; i >= 0; i--)
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
    /// Metoddo para aplicar el efecto segun el tipo
    /// </summary>
    /// <param name="effect"></param>
    private void ApplyEffect(StatusEffect effect)
    {
        switch (effect.effectType)
        {
            case StatusEffect.EffectType.Vulnerable:
                // Ejemplo: El personaje recibe más daño
                Debug.Log($"{gameObject.name} está vulnerable. Recibe más daño.");
                break;
            case StatusEffect.EffectType.Weak:
                // Ejemplo: El personaje hace menos daño
                Debug.Log($"{gameObject.name} está debilitado. Hace menos daño.");
                break;
            case StatusEffect.EffectType.Burn:
                // Ejemplo: El personaje recibe daño continuo por quemadura
                int burnDamage = effect.value;
                Debug.Log($"{gameObject.name} recibe {burnDamage} de daño por quemadura.");
                ReceiveDamage(burnDamage);  // Método que aplicaría el daño
                break;
            case StatusEffect.EffectType.Shield:
                // Ejemplo: El personaje recibe un escudo
                Debug.Log($"{gameObject.name} recibe {effect.value} de escudo.");
                // Aquí podrías añadir el valor del escudo al personaje
                break;
                // Añadir más efectos según sea necesario
        }
    }

    /// <summary>
    ///  Método para aplicar daño al personaje
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(int damage)
    {
        // Lógica para reducir la salud del personaje (a implementar)
        Debug.Log($"{gameObject.name} recibe {damage} de daño.");
    }

}

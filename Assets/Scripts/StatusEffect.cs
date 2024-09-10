using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect 
{
    public enum EffectType { None,Vulnerable, Weak, Burn, Shield, Posion}
    public EffectType effectType;
    //Puede ser dalo extra, reduccion...
    public int value;
    //Nuemro de turnos que dura el efecto
    public int duration;

    public StatusEffect(EffectType type, int value, int duration)
    {
        this.effectType = type;
        this.value = value;
        this.duration = duration;   
    }

    /// <summary>
    /// Reduce la dureacion del efecto en 1 cada turno
    /// </summary>
    public void ReduceDuration()
    {
        duration--;
    }

    /// <summary>
    /// Verifica si el efecto sigue activo
    /// </summary>
    /// <returns></returns>
    public bool IsActive()
    {
        return duration > 0;
    }
}

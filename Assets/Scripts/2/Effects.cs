using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/New Effect")]
public class Effects : ScriptableObject
{
    public EffectType effectType;     // Tipo de efecto
    public Sprite icon;               // Icono del efecto
    public int initialDuration;
    public int duration;              // Duración del efecto en turnos
    public int effectDamage;
    public EffectDisplay effectGO;
    public enum EffectType
    {
        Vulnerable,
        Block,
        Burn
        // Añadir más tipos de efectos aquí
    }

    // Este método se llama en el comienzo de cada turno del enemigo
    public virtual void OnTurnStart(Enemy2 enemy)
    {
        duration--;
        Debug.Log(effectType + " effect on " + enemy.name + " has " + duration + " turns remaining.");
    }

}

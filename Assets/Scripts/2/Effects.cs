using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/New Effect")]
public class Effects : ScriptableObject
{
    public EffectType effectType;     // Tipo de efecto
    public Sprite icon;               // Icono del efecto
    public int duration;              // Duración del efecto en turnos
    public enum EffectType
    {
        Vulnerable,
        Block,
        // Añadir más tipos de efectos aquí
    }

}

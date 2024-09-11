using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/New Card")]
public class Cards : ScriptableObject
{
    public string cardName;    // Nombre de la carta
    public string description; // Descripción de la carta
    public int energyCost;     // Costo de energía para jugar la carta
    public CardType cardType;  // Tipo de carta (Ataque, Defensa, Habilidad)
    public int effectValue;    // Valor del efecto (daño, defensa, etc.)


    // Enum para los tipos de carta
    public enum CardType
    {
        Attack,
        Defense,
        Skill,
        Vulnerable,
        Burn
    }

    // Método virtual que se sobreescribirá en cartas específicas
    public virtual void Play(PlayerEnergy player, List<Enemy2> enemies)
    {
        Debug.Log("Playing card: " + cardName);
    }
}

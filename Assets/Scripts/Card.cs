using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/New Card")]
public class Card : ScriptableObject
{
    public string cardName; //Referencia al nombre de la carta
    public bool isUpgraded; // Indica si la carta esta mejorada o no
    public CardDescription cardDescription; //Referecnia a la descripción de la carta
    public CardAmount cardCost; //Referencia al Coste de la carta
    public CardAmount cardEffect; //Referencia al efecto de la carta
    public CardAmount buffAmount; // Referencia a la cantidad de mejora que aplica la carta
    public Sprite cardImage; //Refereencia a la imagen de la carta
    public CardType cardType; //Referencia la tipo de carta
    //Enumeracion que define los tipos de cartas
    public enum CardType
    {
        Attack,
        Skill,
        Power
    }
    public CardClass cardClass; //Indica la Clase de la carta
    // Enumeración que define las clases de cartas
    public enum CardClass
    {
        fire,
        water,
        ground

    }
    public CardTargetType cardTargetType; //Indica el tipo de objetivo de la carta
    // Enumeración que define los tipos de objetivos de la carta
    public enum CardTargetType
    {
        self,
        enemy
    }

    /// <summary>
    /// Devuelve el valor del coste de la carta segun si esta mejorada o no
    /// </summary>
    /// <returns></returns>
    public int GetCardCostAmount()
    {
        return isUpgraded ? cardCost.upgradedAmount : cardCost.baseAmount;
    }

    /// <summary>
    /// Devuelve el efecto de la carta segun si esta mejorada o no
    /// </summary>
    /// <returns></returns>
    public int GetCardEffectAmount()
    {
        return isUpgraded ? cardEffect.upgradedAmount : cardEffect.baseAmount;
    }

    /// <summary>
    /// Devuelve la descripcion de la carta segun si esta mejorada o no
    /// </summary>
    /// <returns></returns>
    public string GetCardDescriptionAmount()
    {
        return isUpgraded ? cardDescription.upgradedAmount : cardDescription.baseAmount;
    }

    /// <summary>
    /// Devuelve la cantidad de mejoras que se aplica a la carta segun si esta mejorada o no
    /// </summary>
    /// <returns></returns>
    public int GetBuffAmount()
    {
        return isUpgraded ? buffAmount.upgradedAmount : buffAmount.baseAmount;
    }

    //Estructura para lamacenar cantidades base y mejoradas (para costos, efectos,etc.)
    [System.Serializable]
    public struct CardAmount
    {
        public int baseAmount;// Cantidad base
        public int upgradedAmount; //Cantidad mejorada
    }

    //Estructura para alamcenar descripciones base y mejoradas
    [System.Serializable]
    public struct CardDescription
    {
        public string baseAmount;//Descripcion base
        public string upgradedAmount;//Descripcion mejorada
    }

    //Estructura que define un buff, con tipo y cantidad
    [System.Serializable]
    public struct CardBuffs
    {
        //public Buff.Type buffType; //Tipo de mejora que aplica la carta
        public CardAmount buffAmount; //Cantidad de mejora, con valores base y mejorados
    }

}

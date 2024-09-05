using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    //Imagen de la carta
    public Image cardImage;
    //Nombre de la carta
    public TMP_Text cardName;
    //Descripcion de la carta
    public TMP_Text cardDescription;
    //Coste de la carta
    public TMP_Text cardCost;
    //Refrencia a la carta
    private Card card;

    /// <summary>
    /// metodo que configura el prefab de carta con los datos de la carta
    /// </summary>
    /// <param name="card"></param>
    public void Setup(Card card)
    {
        this.card = card;
        //Establece la imagen de la carta
        cardImage.sprite = card.cardImage;
        //Establece el nobre de la carta
        cardName.text = card.cardName;
        //Establece la descricion de la carta
        cardDescription.text = card.GetCardDescriptionAmount();
        //Establece el coste de la carta
        cardCost.text = card.GetCardCostAmount().ToString();


    }
}

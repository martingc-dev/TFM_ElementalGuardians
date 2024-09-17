using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deck2 : MonoBehaviour
{
    //Lista de cartas en el mazo
    public List<Cards> deckCards = new List<Cards>();
    //Lista de cartas de la pila de descarte
    public List<Cards> discardPile = new List<Cards>();
    //Lista de cartas en la mano del jugador
    public List<Cards> handsCards = new List<Cards>();
    //Tamaño maximo de la mano del jugador
    public int handSize = 5;
    public TMP_Text drawPileCountText;
    public TMP_Text discardPileCountText;
    public PlayerEnergy player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Metodo que nicializa el mazo mezclando las cartas
    /// </summary>
    /// <param name="startingDeck"></param>
    public void InitializeDeck(List<Cards> startingDeck)
    {
        deckCards = new List<Cards>(startingDeck);
        ShuffleDeck();
    }

    /// <summary>
    /// Mezcla el mazo de cartas
    /// </summary>
    public void ShuffleDeck()
    {
        for (int i = 0; i < deckCards.Count; i++)
        {
            Cards temp = deckCards[i];
            int randomIndex = Random.Range(i, deckCards.Count);
            deckCards[i] = deckCards[randomIndex];
            deckCards[randomIndex] = temp;
        }
        discardPileCountText.text = discardPile.Count.ToString();
    }

    /// <summary>
    /// Metodo para robar carta
    /// </summary>
    public void DrawCard()
    {
        if (deckCards.Count == 0 && discardPile.Count > 0)
        {
            ReshuffleDiscardPile();
        }

        if (deckCards.Count > 0 && handsCards.Count < handSize)
        {
            Cards drawnCard = deckCards[0];
            deckCards.RemoveAt(0);
            handsCards.Add(drawnCard);
        }
        drawPileCountText.text = deckCards.Count.ToString();
    }

    /// <summary>
    /// metodo que rellena el mazo con las cartas de la pila de descarte y las mezcla
    /// </summary>
    private void ReshuffleDiscardPile()
    {
        deckCards.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }


    /// <summary>
    /// metodo para decartar una carta de la mano
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCard(Cards card)
    {
        if (handsCards.Contains(card))
        {
            handsCards.Remove(card);
            discardPile.Add(card);
        }
    }

    /// <summary>
    /// Metodo para decartar todas las carta de la mano
    /// </summary>
    public void DiscardHand()
    {
        discardPile.AddRange(handsCards);
        handsCards.Clear();
        discardPileCountText.text = discardPile.Count.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Lista de cartas en el mazo
    public List<Card> deckCards = new List<Card>();
    //Lista de cartas de la pila de descarte
    public List<Card> discardPile = new List<Card>();
    //Lista de cartas en la mano del jugador
    public List<Card> handsCards = new List<Card>();
    //Tamaño maximo de la mano del jugador
    public int handSize = 5;

    public Character playerCharacter;

    public Character GetPlayerCharacter()
    {
        return playerCharacter;
    }


    /// <summary>
    /// Metodo que nicializa el mazo mezclando las cartas
    /// </summary>
    /// <param name="startingDeck"></param>
    public void InitializeDeck(List<Card> startingDeck)
    {
        deckCards = new List<Card>(startingDeck);
        ShuffleDeck();
    }

    /// <summary>
    /// Mezcla el mazo de cartas
    /// </summary>
    public void ShuffleDeck()
    {
        for(int i = 0; i < deckCards.Count; i++)
        {
            Card temp = deckCards[i];
            int randomIndex = Random.Range(i,deckCards.Count);
            deckCards[i] = deckCards[randomIndex];
            deckCards[randomIndex] = temp;
        }
    }

    /// <summary>
    /// Metodo para robar carta
    /// </summary>
    public void DrawCard()
    {
        if(deckCards.Count ==0 && discardPile.Count>0)
        {
            ReshuffleDiscardPile();
        }

        if (deckCards.Count > 0 && handsCards.Count < handSize)
        {
            Card drawnCard = deckCards[0];
            deckCards.RemoveAt(0);
            handsCards.Add(drawnCard);
        }
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
    /// Metodo para juegar una carta de la mano
    /// </summary>
    /// <param name="card"></param>
    public void PlayCard(Card card)
    {
        if (handsCards.Contains(card))
        {
            handsCards.Remove(card);
            discardPile.Add(card);
        }
    }

    /// <summary>
    /// metodo para decartar una carta de la mano
    /// </summary>
    /// <param name="card"></param>
    public void DiscardCard(Card card)
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
    }
}

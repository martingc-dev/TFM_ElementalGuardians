using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deck/Starting Deck")]
public class StartingDeck : ScriptableObject
{
    public List<Card> cards; // Lista de cartas iniciales
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    
    public Button endTurnButton;


    


    public GameObject cardPrefab;    // Prefab de la carta
    public Transform handPanel;      // Panel donde se mostrarán las cartas en la mano

    public List<Cards> cardDeck; // Lista de ScriptableObjects de cartas

    // Start is called before the first frame update
    void Start()
    {
        

        // Asignar la función del botón
        endTurnButton.onClick.AddListener(EndTurn);       

        // Mostrar las cartas en la mano
        DisplayHand();
    }

    void DisplayHand()
    {
        foreach (Cards card in cardDeck)
        {
            // Instanciar una carta en el panel de la mano
            GameObject newCard = Instantiate(cardPrefab, handPanel);

            // Obtener el componente CardDisplay del prefab e inicializarlo con la carta
            CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
            cardDisplay.DisplayCard(card);
        }
    }

   

    void EndTurn()
    {
        // Lógica para finalizar el turno del jugador
        Debug.Log("Turn ended");
        // Aquí eventualmente se reducirá la energía o se pasará el turno al enemigo
    }

}

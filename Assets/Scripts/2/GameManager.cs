using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    
    public Button endTurnButton;




    //Referencia al BattleManager
    public CombatManager battleManager;
    public GameObject cardPrefab;    // Prefab de la carta
    public Transform handPanel;      // Panel donde se mostrarán las cartas en la mano

  
    public List<Cards> cardDeck; // Lista de ScriptableObjects de cartas

    // Start is called before the first frame update
    void Start()
    {
        

        // Asignar la función del botón
        ///endTurnButton.onClick.AddListener(EndTurn);       

        // Mostrar las cartas en la mano
        //DisplayHand();
    }

    public void DisplayHand()
    {
        //Limpia el panel de la mano actual
        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }


        if (battleManager != null && battleManager.playerDeck != null)
        {
            // Añade las cartas actuales a la mano
            foreach (Cards card in battleManager.playerDeck.handsCards)
            {
                if (card != null)
                {
                    // Instancia el prefab de carta en el panel de la mano
                    GameObject cardGO = Instantiate(cardPrefab, handPanel);

                    // Configura el prefab de carta con los datos de la carta
                    CardDisplay cardUI = cardGO.GetComponent<CardDisplay>();

                    if (cardUI != null)
                    {
                        cardUI.DisplayCard(card);
                        Debug.Log($"Carta '{card.cardName}' añadida a la mano.");
                    }
                    else
                    {
                        Debug.LogError("CardUI component missing on cardPrefab.");
                    }
                }
                else
                {
                    Debug.LogWarning("Carta en la mano es null.");
                }

            }
        }
        else
        {
            Debug.LogWarning("battleManager o playerDeck no están asignados.");
        }
        //foreach (Cards card in cardDeck)
        //{
        //    // Instanciar una carta en el panel de la mano
        //    GameObject newCard = Instantiate(cardPrefab, handPanel);

        //    // Obtener el componente CardDisplay del prefab e inicializarlo con la carta
        //    CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
        //    cardDisplay.DisplayCard(card);
        //}
    }



    void EndTurn()
    {
        // Lógica para finalizar el turno del jugador
        Debug.Log("Turn ended");
        // Aquí eventualmente se reducirá la energía o se pasará el turno al enemigo
    }

}

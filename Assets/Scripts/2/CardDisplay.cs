using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TMP_Text nameText;      // Referencia al texto del nombre de la carta
    public TMP_Text descriptionText; // Referencia al texto de la descripción

    public Button playButton;
    private Cards cards;
    private PlayerEnergy playerEnergy;
    private Enemy2 enemy;         // Referencia al enemigo
    public Effects VulnerableEffect;

    private void Start()
    {
        playerEnergy = FindObjectOfType<PlayerEnergy>(); // Encontrar el sistema de energía en la escena
        enemy = FindObjectOfType<Enemy2>();               // Encontrar al enemigo
        playButton.onClick.AddListener(PlayCard);        // Añadir el listener al botón
    }


    // Función para mostrar la carta
    public void DisplayCard(Cards card)
    {
        this.cards = card;
        nameText.text = card.cardName;
        descriptionText.text = card.description;
    }

    // Método para jugar la carta
    void PlayCard()
    {
        // Verificar si el jugador tiene suficiente energía para jugar la carta
        if (playerEnergy.UseEnergy(cards.energyCost))
        {
            // Aplicar el efecto según el tipo de carta
            switch (cards.cardType)
            {
                case Cards.CardType.Attack:
                    enemy.TakeDamage(cards.effectValue);   // Hacer daño al enemigo
                    Debug.Log("Attacked enemy for " + cards.effectValue + " damage.");
                    break;
                case Cards.CardType.Defense:
                    playerEnergy.GainBlock(cards.effectValue);  // Ganar bloqueo
                    Debug.Log("Gained " + cards.effectValue + " defense.");
                    break;
                case Cards.CardType.Skill:
                    // Lógica para habilidades especiales
                    Debug.Log("Skill used.");
                    break;
                case Cards.CardType.Vulnerable:
                    enemy.ApplyEffect(VulnerableEffect);  // Aplicar el efecto de vulnerable
                    enemy.isVulnerable = true;
                    Debug.Log("Applied vulnerable to enemy.");
                    break;
            }

            Destroy(gameObject); // Destruir la carta después de usarla
        }
        else
        {
            Debug.Log("Not enough energy to play this card.");
        }
    }
}

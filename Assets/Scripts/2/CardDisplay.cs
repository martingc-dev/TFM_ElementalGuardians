using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class CardDisplay : MonoBehaviour
{
    public TMP_Text nameText;      // Referencia al texto del nombre de la carta
    public TMP_Text descriptionText; // Referencia al texto de la descripción

    public Button playButton;
    private Cards cards;
    private PlayerEnergy playerEnergy;
    private Enemy2 enemy;         // Referencia al enemigo
    public Effects VulnerableEffect;
    public Effects burnEffect;
    public Deck2 deck;

    public bool areaAttackCard = false;

    private void Start()
    {
        playerEnergy = FindObjectOfType<PlayerEnergy>(); // Encontrar el sistema de energía en la escena
        enemy = FindObjectOfType<Enemy2>();               // Encontrar al enemigo
        playButton.onClick.AddListener(PlayCard);        // Añadir el listener al botón
        deck = FindObjectOfType<Deck2>();
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
                    //enemy.ApplyEffect(VulnerableEffect);  // Aplicar el efecto de vulnerable
                    ApplyBuff(Buff.Type.vulnerable);
                    enemy.isVulnerable = true;
                    Debug.Log("Applied vulnerable to enemy.");
                    break;
                case Cards.CardType.Burn:
                    enemy.ApplyEffect(burnEffect);
                    enemy.isBurned = true;
                    //ApplyEffectUI(Effects.EffectType.Burn);
                    //enemy.ApplyBurn(10,cards.effectValue);
                    Debug.Log("Applied Burn to enemy.");
                    break;
                
            }
            gameObject.SetActive(false);
            deck.handsCards.Remove(cards);
            deck.DiscardCard(cards);
            // Destroy(gameObject); // Destruir la carta después de usarla
        }
        else
        {
            Debug.Log("Not enough energy to play this card.");
        }
    }

    private void ApplyBuff(Buff.Type t)
    {
        enemy.AddBuff(t, cards.effectValue);
    }
    

    private void ApplyEffectUI(Effects.EffectType type)
    {
        //enemy.DisplayEffectUI(type, cards.effectValue);
    }
}

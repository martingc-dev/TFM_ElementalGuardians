using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    //Referencia al mazo del jugador
    public Deck playerDeck;
    // Referencia al ScriptableObject con el mazo inicial
    public StartingDeck startingDeck; 
    //Eenrgia maxima del jugador
    public int maxEnergy = 3;
    //Energia actual del jugador
    public int currentEnergy;
    public TMP_Text energyText;

    public UIManager uiManager;

    //Salud del enemigo
    public int enemyHealth = 30;

    private void Start()
    {
        StartBattle();
    }

    /// <summary>
    /// Metodo que inicia la batalla
    /// </summary>
    public void StartBattle()
    {
       
        if (startingDeck != null)
        {
            //Inicializa el mazo del jugador
            playerDeck.InitializeDeck(startingDeck.cards); 
        }
        else
        {
            Debug.LogError("StartingDeck no asignado en BattleManager.");
        }
        StartTurn();
    }

    /// <summary>
    /// Metodo para iniciar el turno
    /// </summary>
    public void StartTurn()
    {
        //Reinicia la energia al maximo
        currentEnergy = maxEnergy;
        // actualizar la UI después de reiniciar la energía
        UpdateEnergyUI(); 
        //Descarta las cartas actuales de la mano
        playerDeck.DiscardHand();
        //Roba ua nueva mano
        DrawHand();
    }

    /// <summary>
    /// metodo para robar una mano  completa
    /// </summary>
    public void DrawHand()
    {
        for(int i = 0; i<playerDeck.handSize; i++)
        {
            playerDeck.DrawCard();
        }

        //actualiza la interfaz de usuario para mostrar las cartas robadas en la mano
        UpdateHandUI();
        Debug.Log("Cartas robadas y mano actualizada.");
    }

    /// <summary>
    /// Metodo para jugar una carta del jugador
    /// </summary>
    /// <param name="card"></param>
    public void PlayCard(Card card)
    {
        if(currentEnergy >= card.GetCardCostAmount())
        {
            //Mueve la carta jugada a la pila de descarte
            playerDeck.PlayCard(card);
            //Aplica los efectos de la carta
            ApplyCardEffects(card);
            //Rduce la energia del jugador
            currentEnergy -=card.GetCardCostAmount();

            //Actualiza la interfaz de energia y la mano
            UpdateEnergyUI();
            UpdateHandUI();
        }
        else
        {
            Debug.Log("No hay suficiente energía para jugar esta carta.");
        }
    }

    /// <summary>
    /// Metodo que aplica efectos de un acarta jugada
    /// </summary>
    /// <param name="card"></param>
    private void ApplyCardEffects(Card card)
    {

        // Aquí se deben implementar los efectos de la carta.
        // Por ejemplo, si es una carta de ataque, reducir la vida del enemigo.
        // Si es una carta de mejora, aumentar las estadísticas del jugador, etc.
        switch (card.cardType)
        {
            case Card.CardType.Attack:
                ApplyAttack(card);
                break;
            case Card.CardType.Skill:
                ApplySkill(card);
                break;
            case Card.CardType.Power:
                ApplyPower(card);
                break;
            default:
                Debug.LogWarning("Tipo de carta desconocido.");
                break;
        }
    }

    /// <summary>
    /// Metodo para carta de ataque
    /// </summary>
    /// <param name="card"></param>
    private void ApplyAttack(Card card)
    {
        
        int damage = card.GetCardEffectAmount();
        enemyHealth -= damage;
        Debug.Log($"Ataque: {card.cardName} inflige {damage} de daño. Salud del enemigo: {enemyHealth}");

        
        // Aquí podrías actualizar la UI para mostrar la salud del enemigo.
    }

    /// <summary>
    /// Metodo para aplicar efectos
    /// </summary>
    /// <param name="card"></param>
    private void ApplySkill(Card card)
    {
        int skillEffect = card.GetCardEffectAmount();
        Debug.Log($"Habilidad: {card.cardName} aplica un efecto de {skillEffect}.");

        // Ejemplo de añadir energía o aplicar debuffs
        if (card.cardClass == Card.CardClass.fire)
        {
            // Efecto específico para cartas de fuego
            Debug.Log("Efecto de fuego aplicado.");
        }
        // Añadir más lógica según la clase y tipo de habilidad
    }

    private void ApplyPower(Card card)
    {
        // Ejemplo de aplicar efectos de poder
        int powerEffect = card.GetCardEffectAmount();
        Debug.Log($"Poder: {card.cardName} aplica un efecto de {powerEffect}.");

        // Ejemplo de efectos persistentes o modificadores
    }

    private void ApplyDebuff(Card card)
    {
        // Aplicar debuffs a enemigos o al jugador
        Debug.Log($"Debuff aplicado por {card.cardName}.");
    }

    /// <summary>
    /// Metodo para finalizar el turno del juagdor
    /// </summary>
    public void EndTurn()
    {
        // Al final del turno, podemos implementar efectos de fin de turno, etc.
        // Por ahora, simplemente comienza un nuevo turno.
        StartTurn();
    }

    /// <summary>
    /// Metodo que actualiza la inerfaz de la mano del juagdor
    /// </summary>
    private void UpdateHandUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateHandUI();
        }
        else
        {
            Debug.LogWarning("uiManager no está asignado en BattleManager.");
        }
        Debug.Log("Mano actualizada.");
    }

    /// <summary>
    /// metodo actualiza la interfaz de energía del jugador.
    /// </summary>
    private void UpdateEnergyUI()
    {
        // Verifica si energyText está asignado
        if (energyText != null) 
        {
            energyText.text = $"Energía: {currentEnergy}/{maxEnergy}";
        }
        else
        {
            Debug.LogWarning("energyText no está asignado en BattleManager.");
        }
    }


}

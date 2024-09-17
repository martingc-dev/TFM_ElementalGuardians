using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //Referencia al BattleManager
    public CombatManager battleManager;
    //Panel donde se muestran las cartas en la mano
    public Transform handPanel;
    //Prefab de la carta de la mano
    public GameObject cardPrefab;
    //Texto para mostrat la energia del juagdor
    public TMP_Text energyText;
    // Nueva referencia para el Slider de salud
    public Slider healthSlider;




    // Start is called before the first frame update
    void Start()
    {
        // Actualiza la UI al inicio del juego
        UpdateHandUI();
        UpdateEnergyUI();
        UpdateHealthUI(); // Asegúrate de actualizar la barra de vida al inicio
    }

    /// <summary>
    /// Actualiza la interfaz de la mano del jugador
    /// </summary>
    public void UpdateHandUI()
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
                if(card != null)
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

    }

    /// <summary>
    /// Metodo que actualiza la interfaz de energia del juagdor
    /// </summary>
    public void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            //energyText.text = $"Energía: {battleManager.currentEnergy}/{battleManager.maxEnergy}";
        }
        else
        {
            Debug.LogWarning("energyText no está asignado en UIManager.");
        }
    }


    /// <summary>
    /// Metodo que actualiza la barra de vida del jugador
    /// </summary>
    public void UpdateHealthUI()
    {
        //if (healthSlider != null && battleManager != null && battleManager.playerDeck != null)
        //{
        //    //Character playerCharacter = battleManager.playerDeck.GetPlayerCharacter();
        //    //if (playerCharacter != null)
        //    //{
        //    //    healthSlider.maxValue = playerCharacter.GetMaxHealth();
        //    //    healthSlider.value = playerCharacter.GetCurrentHealth();
        //    //}
        //    else
        //    {
        //        Debug.LogWarning("PlayerCharacter no está asignado en BattleManager.");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("healthSlider o battleManager no están asignados en UIManager.");
        //}
    }

}

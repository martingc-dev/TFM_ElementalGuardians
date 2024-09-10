using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;            // Vida del jugador
    public TMP_Text healthText;        // Texto de la UI para mostrar la vida
    public int maxEnergy = 3;      // Energía máxima del jugador
    public int currentEnergy;      // Energía actual del jugador
    public TMP_Text energyText;        // Texto de la UI para mostrar la energía
    public int currentBlock;        // Bloqueo actual del jugador
    public TMP_Text blockText;  // Texto de la UI para el bloqueo


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy; // Inicializar energía
        currentBlock = 0;           // Inicializar el bloqueo
        UpdateUI();          // Actualizar la UI al inicio
    }

    public void ReceiveDamage(int amount)
    {
        if (currentBlock >= amount)
        {
            currentBlock -= amount;
        }
        else
        {
            int damageAfterBlock  =amount - currentBlock;
            currentBlock = 0;
            currentHealth -= damageAfterBlock;
        }

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para gastar energía
    public bool UseEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            UpdateUI();
            return true; // Energía suficiente
        }
        return false; // Energía insuficiente
    }


    // Método para ganar bloqueo
    public void GainBlock(int amount)
    {
        currentBlock += amount;
        UpdateUI();
    }

    //// Método para recibir daño (considerando el bloqueo)
    //public void TakeDamage(int damage)
    //{
    //    int damageAfterBlock = Mathf.Max(damage - currentBlock, 0);  // Calcular el daño restante después del bloqueo
    //    currentBlock = Mathf.Max(currentBlock - damage, 0);          // Reducir el bloqueo según el daño recibido
    //    Debug.Log("Player takes " + damageAfterBlock + " damage after block.");

    //    // Aquí puedes reducir la vida del jugador si implementas un sistema de vida
    //    UpdateUI();
    //}

    // Método para actualizar la UI de energía y bloqueo
    void UpdateUI()
    {
        healthText.text = "Health: " + currentHealth;
        energyText.text = "Energy: " + currentEnergy;
        blockText.text = "Block: " + currentBlock;
    }

    

    // Método para resetear la energía al máximo (por ejemplo, al comienzo de un turno)
    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        UpdateUI();
    }


    // Método cuando el jugador muere
    void Die()
    {
        Debug.Log("Player defeated!");
        // Aquí puedes implementar la lógica cuando el jugador muere
    }
}

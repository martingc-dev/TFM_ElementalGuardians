using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy2 : MonoBehaviour
{

    public int health = 50;     // Vida del enemigo
    public TMP_Text healthText;     // Texto de la UI que muestra la vida del enemigo
    public bool isVulnerable = false; // Indica si el enemigo es vulnerable

    public GameObject statusIconsPanel;  // Referencia al panel de iconos de efectos
    public List<Effects> effectList = new List<Effects>();   // Lista de efectos activos

    private Dictionary<Effects.EffectType, GameObject> activeEffectsIcons = new Dictionary<Effects.EffectType, GameObject>();

    //private GameObject currentVulnerableIcon; // Instancia del icono de vulnerabilidad


    public int damage = 10;  // Daño que el enemigo inflige


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();
        UpdateStatusIcons();
    }

    public void AttackPlayer(PlayerEnergy player)
    {
        player.ReceiveDamage(damage);
    }


    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        int damageAfterVulnerability = isVulnerable ? Mathf.RoundToInt(amount * 1.5f) : amount;
        health -= damageAfterVulnerability;
        UpdateHealthUI();

        // Después de recibir daño, resetear el estado de vulnerabilidad
        effectList.RemoveAll(e => e.effectType == Effects.EffectType.Vulnerable);
        UpdateStatusIcons();

        if (health <= 0)
        {
            Die();
        }
    }

    // Método para aplicar el efecto de vulnerable
    public void ApplyVulnerable()
    {
        isVulnerable = true;
        Debug.Log("Enemy is now vulnerable.");
        UpdateStatusIcons();
    }

    // Método para aplicar un efecto
    public void ApplyEffect(Effects effect)
    {
        if (!effectList.Contains(effect))
        {
            effectList.Add(effect);
            Debug.Log("Applied " + effect.effectType.ToString() + " effect.");
            UpdateStatusIcons();
        }
    }

    // Método para eliminar un efecto
    public void RemoveEffect(Effects effect)
    {
        if (effectList.Contains(effect))
        {
            effectList.Remove(effect);
            Debug.Log("Removed " + effect.effectType.ToString() + " effect.");
            UpdateStatusIcons();
        }
    }


    // Método para actualizar los iconos de efectos en la UI
    void UpdateStatusIcons()
    {
        // Limpiar iconos antiguos
        foreach (Transform child in statusIconsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Mostrar los iconos de efectos activos
        foreach (Effects effect in effectList)
        {
            GameObject iconObject = new GameObject("EffectIcon");
            Image iconImage = iconObject.AddComponent<Image>();
            iconImage.sprite = effect.icon;
            iconObject.transform.SetParent(statusIconsPanel.transform, false);

            // Añadir a la lista de iconos activos
            if (!activeEffectsIcons.ContainsKey(effect.effectType))
            {
                activeEffectsIcons.Add(effect.effectType, iconObject);
            }
        }
    }


    // Actualizar la UI de la vida del enemigo
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // Método cuando el enemigo muere
    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Destruye al enemigo cuando su vida llega a 0
    }
}

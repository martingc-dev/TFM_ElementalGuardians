using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class Enemy2 : MonoBehaviour
{

    public int health = 50;     // Vida del enemigo
    public TMP_Text healthText;     // Texto de la UI que muestra la vida del enemigo
    public bool isVulnerable = false; // Indica si el enemigo es vulnerable

    public GameObject statusIconsPanel;  // Referencia al panel de iconos de efectos
    public List<Effects> effectList = new List<Effects>();   // Lista de efectos activos

    [Header("Effects")]
    public Effects burn;
    public Buff vulnerable;

   

    //private GameObject currentVulnerableIcon; // Instancia del icono de vulnerabilidad


    public int damage = 10;  // Daño que el enemigo inflige
    public bool isDefeated = false;
    public string enemyName;

    private int burnDamage;
    public int burnTurns;
    public bool isBurned = false;

    //Prefab de la UI que se muestra cuando un Biff es aplicado
    public GameObject effectPrefab;
    //Transform padre dodne los buff seran instanciados en la UI
    public Transform effectParent;


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthUI();
       
    }

    public virtual void AttackPlayer(PlayerEnergy player)
    {
        player.ReceiveDamage(damage);
        EvaluateBuffAtTurnEnd();
    }


    // Método para recibir daño
    public virtual void TakeDamage(int amount)
    {
        int damageAfterVulnerability = isVulnerable ? Mathf.RoundToInt(amount * 1.5f) : amount;
        health -= damageAfterVulnerability;
        UpdateHealthUI();        
        

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
        
    }

    public void ApplyBurn(int damage, int turns)
    {
        isBurned = true;
        burnDamage = damage;
        burnTurns = turns;
        StartCoroutine(ApplyBurnOverTime());
    }

    private IEnumerator ApplyBurnOverTime()
    {
        while (burnTurns > 0)
        {
            yield return new WaitForSeconds(1f); // Espera un turno (un segundo para este ejemplo)
            health -= burnDamage;
            burnTurns--;

            Debug.Log("Burn tick: " + burnDamage + " damage. Remaining health: " + health);
        }
    }

    // Método para aplicar un efecto
    public void ApplyEffect(Effects effect)
    {
        
        effectList.Add(effect);
        DisplayEffectUI(effect, effect.initialDuration);
    }

    public void AddBuff(Buff.Type type, int amount)
    {
        if(type == Buff.Type.vulnerable)
        {
            if (vulnerable.buffValue <= 0)
            {
                vulnerable.buffGo = Instantiate(effectPrefab, effectParent).GetComponent<BuffUI>();
                Debug.Log("Buff instaciado");
            }
            vulnerable.buffValue += amount;
            vulnerable.buffGo.DisplayBuff(vulnerable);
        }
    }

    public void EvaluateBuffAtTurnEnd()
    {
        if(vulnerable.buffValue > 0)
        {
            vulnerable.buffValue -= 1;
            vulnerable.buffGo.DisplayBuff(vulnerable);
            if(vulnerable.buffValue <= 0)
            {
                Destroy(vulnerable.buffGo.gameObject);
            }
        }
    }

    /// <summary>
    /// LLamada al comienzo del turno del enemigo
    /// </summary>
    public void OnTurnStart()
    {
        ProcessEffects();
    }

    private void ProcessEffects()
    {
        List<Effects> effectToRemove = new List<Effects>();

        foreach (Effects effect in effectList)
        {
            effect.OnTurnStart(this);

            if (isBurned)
            {
                TakeDamage(effect.effectDamage);
            }

            if (effect.duration <= 0)
            {
                effectToRemove.Add(effect);
            }
        }

        foreach (Effects effect in effectToRemove)
        {
            effectList.Remove(effect);
        }

       
    }

    // Método para eliminar un efecto
    public void RemoveEffect(Effects effect)
    {
        if (effectList.Contains(effect))
        {
            effectList.Remove(effect);
            Debug.Log("Removed " + effect.effectType.ToString() + " effect.");
            
        }
    }

    public void DisplayEffectUI(Effects effects, int amount)
    {
        Debug.Log("Displaying effect: " + effects.effectType);
        if (effects == burn)
        {
            //if(burn.duration <= 0)
            //{
            //    burn.effectGO = Instantiate(effectPrefab, effectParent).GetComponent<EffectDisplay>();
            //}

            if (burn.effectGO == null)  // Asegúrate de que effectGO sea nulo antes de instanciarlo
            {
                Debug.Log("Instantiating burn effect UI.");
                burn.effectGO = Instantiate(effectPrefab, effectParent).GetComponent<EffectDisplay>();
                burn.duration = 0;

                
            }
            
            burn.duration += amount;
            burn.effectGO.DisplayEffect(burn);
            Debug.Log("Burn effect UI displayed with " + burn.duration + " turns remaining.");
        }
    }


   


    // Actualizar la UI de la vida del enemigo
    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // Método cuando el enemigo muere
    public virtual void Die()
    {
        Debug.Log("Enemy defeated!");
        isDefeated = true;
        Destroy(gameObject); // Destruye al enemigo cuando su vida llega a 0
    }
}

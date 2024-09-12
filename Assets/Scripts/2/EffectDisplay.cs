using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectDisplay : MonoBehaviour
{

    public Image effectImage;
    public TMP_Text effectTurnText;

    // Función para mostrar la carta
    public void DisplayEffect(Effects effects)
    {
        effectTurnText.text = effects.duration.ToString();
        effectImage.sprite = effects.icon;
    }
}

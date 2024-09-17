using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffUI : MonoBehaviour
{
    public Image buffImage;
    public TMP_Text buffAmountText;
    
    public void DisplayBuff(Buff b)
    {
        buffImage.sprite = b.buffIcon;
        buffAmountText.text = b.buffValue.ToString();
    }
}

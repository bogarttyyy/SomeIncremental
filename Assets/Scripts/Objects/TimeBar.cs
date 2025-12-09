using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    private Image bar;
    
    private void Awake()
    {
        bar = GetComponent<Image>();
    }
    
    public void SetFillAmount(float amount)
    {
        bar.fillAmount = amount;
    }
}

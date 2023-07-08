using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image fill;
    public void UpdateHealthbar(ushort currentHealth, ushort maxHealth)
    {
        fill.fillAmount = (float)currentHealth / maxHealth;
    }
}

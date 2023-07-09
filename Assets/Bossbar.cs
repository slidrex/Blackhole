using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bossbar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private GameObject bossBarObject;
    public void FillBar(ushort currentAmount, ushort maxAmount)
    {
        _fill.fillAmount = (float)currentAmount/maxAmount;
    }
    public void ActivateBar(bool active)
    {
        _fill.fillAmount = 1;
        bossBarObject.SetActive(active);
    }
}

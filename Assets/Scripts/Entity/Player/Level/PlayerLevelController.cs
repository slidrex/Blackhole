using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelController : MonoBehaviour
{
    public int CurrentLevel { get; private set; }
    public int BaseExpPerLevel;
    private int currentExp;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _expFill;
    protected void OnLevelUp(int newLevel)
    {
        Player.Instance.OnLevelUp();
    }
    public void AddExp(int exp)
    {
        currentExp += exp;
        int nextLevelPrice = GetNextLevelExpCost();
        while (currentExp > nextLevelPrice)
        {
            CurrentLevel ++;
            OnLevelUp(CurrentLevel);
            _levelText.text = CurrentLevel.ToString();
            currentExp -= nextLevelPrice;
            nextLevelPrice = GetNextLevelExpCost();
        }
        _expFill.fillAmount = (float)currentExp / GetNextLevelExpCost();
    }
    private int GetNextLevelExpCost()
    {
        return (int)(BaseExpPerLevel * Mathf.Pow(1.15f, (CurrentLevel + 1)));
    }
    public void ResetLevel()
    {
        _expFill.fillAmount = 0;
        CurrentLevel = 0;
        currentExp = 0;
        _levelText.text = CurrentLevel.ToString();
    }
}

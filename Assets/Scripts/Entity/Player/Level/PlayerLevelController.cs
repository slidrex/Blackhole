using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Player;

public class PlayerLevelController : MonoBehaviour
{
    public int CurrentLevel { get; private set; }
    public int BaseExpPerLevel;
    public int currentExp;
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
            
            currentExp -= nextLevelPrice;
            nextLevelPrice = GetNextLevelExpCost();
        }
        UpdateViews();  
    }
    public void FeedStats(PlayerLevelStats stats)
    {
        CurrentLevel = stats.level;
        currentExp = stats.exp;
        UpdateViews();
    }
    private int GetNextLevelExpCost()
    {
        return (int)(BaseExpPerLevel * Mathf.Pow(1.15f, CurrentLevel + 1));
    }
    public void ResetLevel()
    {
        CurrentLevel = 0;
        currentExp = 0;
        UpdateViews();
    }
    private void UpdateViews()
    {
        _expFill.fillAmount = (float)currentExp / GetNextLevelExpCost();
        _levelText.text = CurrentLevel.ToString();
    }
}

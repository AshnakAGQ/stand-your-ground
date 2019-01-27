using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int StartingGold = 0;
    [SerializeField] private TextMeshProUGUI GUI;
    public bool canPurchase = true;

    private void Start()
    {
        GUI.text = "Gold: " + StartingGold;
    }

    public void PurchaseItem(TowerAI item)
    {
        if (canPurchase && item.cost <= StartingGold)
        {
            Instantiate(item);
            StartingGold -= item.cost;
            GUI.text = "Gold: " + StartingGold;
        }
    }

    public void AddGold(int amount)
    {
        StartingGold += amount;
        GUI.text = "Gold: " + StartingGold;
    }
}

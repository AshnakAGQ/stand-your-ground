using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int StartingGold = 0;
    [SerializeField] private TextMeshProUGUI GUI;
    TowerAI currentItem;
    public bool canPurchase = true;

    private void Start()
    {
        GUI.text = "Gold: " + StartingGold;
    }

    public void PurchaseItem(TowerAI item)
    {
        if (!canPurchase)
        {
            Destroy(currentItem.gameObject);
            canPurchase = true;
        }
        if (canPurchase && item.cost <= StartingGold)
        {
            currentItem = Instantiate(item);
        }
    }

    public void ConfirmPurchase()
    {
        StartingGold -= currentItem.cost;
        GUI.text = "Gold: " + StartingGold;
        if (currentItem.cost <= StartingGold)
        {
            currentItem = Instantiate(currentItem);
        }
        else canPurchase = true;
    }

    public void AddGold(int amount)
    {
        StartingGold += amount;
        GUI.text = "Gold: " + StartingGold;
    }
}

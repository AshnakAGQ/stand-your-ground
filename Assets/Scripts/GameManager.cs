using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int StartingGold = 0;
    [SerializeField] private TextMeshProUGUI GUI;
    [SerializeField] public GameObject PauseScreen;
    [SerializeField] public uint playerHealth = 10;
    [SerializeField] public Vector3 spawn = new Vector3(1, 1, 0);
    public int wave;
    public bool paused;
    public bool canPurchase = true;
    TowerAI currentItem;

    public float testTimer = 0;
    public float testRate = 5;

    void Start()
    {
        GUI.text = "Gold: " + StartingGold;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Health
        if (playerHealth == 0)
            GameOver();

        // Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canPurchase)
            {
                Destroy(currentItem.gameObject);
                canPurchase = true;
            }
            else Pause();
        }

        Test();
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

    void Pause()
    {
        paused = !paused;
        Time.timeScale = 1 - Time.timeScale;
        PauseScreen.gameObject.SetActive(paused);
    }

    public void AddGold(int amount)
    {
        StartingGold += amount;
        GUI.text = "Gold: " + StartingGold;
    }

    public void MainMenu()
    {
        Pause();
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    void Test()
    {
        if (testTimer >= testRate)
        {
            Instantiate(Resources.Load("HumanCreep"), spawn, Quaternion.identity);
            testTimer = 0;
        }
        else testTimer += Time.deltaTime;
    }
}

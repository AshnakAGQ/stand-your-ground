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
    [SerializeField] public int playerHealth = 10;
    [SerializeField] public Vector3 spawn = new Vector3(1, 1, 0);
    [SerializeField] public GameObject waveScreen;
    [SerializeField] public GameObject gameOverScreen;
    public int wave = 0;
    public bool paused;
    public bool canPurchase = true;
    TowerAI currentItem;
    [SerializeField] public uint creepCount = 0;

    public float testTimer = 0;
    public float testRate = 5;

    public Spawner creepSpawner;


    void Start()
    {
        Time.timeScale = 1.5f;
        GUI.text = "HP: " + playerHealth + "\nGold: " + StartingGold;
    }

    private void Awake()
    {
        
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

        //creepSpawner.SpawnWave();

        //Test();
    }

    public void DamagePlayer(int damage)
    {
        if (damage > playerHealth)
        {
            playerHealth = 0;
        }
        else
        {
            playerHealth -= damage;
        }
        GUI.text = "HP: " + playerHealth + "\nGold: " + StartingGold;
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
        GUI.text = "HP: " + playerHealth + "\nGold: " + StartingGold;
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
        GUI.text = "HP: " + playerHealth + "\nGold: " + StartingGold;
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
        gameOverScreen.GetComponent<TextMeshProUGUI>().text = "Game Over!\nYou made it to level 0!";
        gameOverScreen.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void advanceLevel()
    {
        waveScreen.GetComponent<TextMeshProUGUI>().text = "Wave " + wave + " Complete!";
        waveScreen.gameObject.transform.parent.gameObject.SetActive(true);
        wave += 1;
    }

    void Test()
    {
        if (testTimer >= testRate)
        {
            Instantiate(Resources.Load("BroodMother"), spawn, Quaternion.identity);
            testTimer = 0;
        }
        else testTimer += Time.deltaTime;
    }
}

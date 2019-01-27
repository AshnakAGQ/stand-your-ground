using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    void MainMenu() { SceneManager.LoadScene(0); }
    void Quit() { Application.Quit(); }
    
}


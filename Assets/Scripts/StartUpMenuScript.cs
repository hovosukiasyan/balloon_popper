using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUpMenuScript : MonoBehaviour
{
    public Button startButton;


    void Start()
    {
        // Add listeners to the buttons
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Game");
    }

}

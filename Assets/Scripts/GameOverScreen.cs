using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text gameOverText;

    public void Setup()
    {
        gameObject.SetActive(true);
        // gameOverText.text = "Game Over";
    }

    public void RestartButton(){
        SceneManager.LoadScene("Game");

    }
}
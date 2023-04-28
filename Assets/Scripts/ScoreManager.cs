using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    private void Update() 
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    // Method to increment the score by 1
    public void IncrementScore()
    {
        score++;
    }
    
    public void ResetScore()
    {
        score = 0;
    }
    public int GetScore()
    {
        return score;
    }
}

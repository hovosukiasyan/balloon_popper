using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    
    // Method to increment the score by 1
    public void IncrementScore()
    {
        score++;
    }
}

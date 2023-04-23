using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public BalloonUpScript orignal;
    public float speed;

    public GameOverScreen gameOverScreen;
    
    public TextMeshProUGUI scoreText; // text which shows the score
    public int numberOfBalloons;
    public float spawnInterval = 0.7f; // the time interval between spawns in seconds
    public float startTime = 0f; // the time in seco

        void Start()
    {
        // Start invoking the SpawnBalloons() method at the specified time interval
        InvokeRepeating("SpawnBalloons", startTime, spawnInterval);
    }


    void SpawnBalloons()
    {
        // Get the length of the object this script is attached to
        float objectLength = transform.localScale.x;
        // Get the current speed of the original balloon




        for (int i = 0; i < numberOfBalloons; i++)
        {
            // Adjust the x-coordinate of the random position to spawn balloons along the length of the object
            Vector3 randomPosition = new Vector3(Random.Range(-objectLength/2f, objectLength/2f), -8f, 0);
            GameObject balloon = Instantiate(balloonPrefab, randomPosition, Quaternion.identity); //creating the clone of the balloon
            BalloonUpScript balloonScript = balloon.GetComponent<BalloonUpScript>(); 
            balloonScript.gameOverScreen = gameOverScreen; //getting the gameOverScreen reference for the clone
            balloonScript.scoreText = scoreText;
            balloonScript.speed = orignal.speed;

            
                        
        }
    }

    
}



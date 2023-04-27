using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedBalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public RedBalloonUpScript original;
    public float speed;

    public GameOverScreen gameOverScreen;
    
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
        // Get the screen width in world units
        float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f;
        Debug.Log("scrwd" +screenWidth);
        float fifteenPercent = (screenWidth*15)/100;
        original.scaleCalculator(screenWidth);






        // Get the current speed of the original balloon
        float balloonSpeed = original.speed;


        
        for (int i = 0; i < numberOfBalloons; i++)
        {
            // Adjust the x-coordinate of the random position to spawn balloons along the width of the screen
            Vector3 randomPosition = new Vector3(Random.Range(-screenWidth/2f+fifteenPercent, screenWidth/2f-fifteenPercent), -10f, 0);
            GameObject balloon = Instantiate(balloonPrefab, randomPosition, Quaternion.identity); //creating the clone of the balloon
            RedBalloonUpScript redBalloonScript = balloon.GetComponent<RedBalloonUpScript>(); 
            redBalloonScript.gameOverScreen = gameOverScreen; //getting the gameOverScreen reference for the clone
            redBalloonScript.speed = original.speed;

            
                        
        }
    }
    
}



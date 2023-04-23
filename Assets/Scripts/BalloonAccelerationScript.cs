using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonAccelerationScript : MonoBehaviour
{
private BalloonUpScript balloonScript; // Reference to BalloonUpScript
    private float timer = 0.0f;
    public float interval = 5.0f; // The interval at which to increase the speed
    
    public float increaseAmount = 0.025f; // The amount by which to increase the speed


    void Start()
    {
        // Find the BalloonUpScript component on a game object in the scene
        balloonScript = FindObjectOfType<BalloonUpScript>();
    }

    void Update()
    {
        // Access the speed field from BalloonUpScript
       if (balloonScript != null)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                balloonScript.IncreaseSpeed(increaseAmount);
                timer = 0.0f;
            }
        }
    }
}




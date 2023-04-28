using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BalloonUpScript : MonoBehaviour
{
    public GameOverScreen gameOverScreen; // GameOverScreen object
    public float speed = 10.0f; // The speed at which the object will move
    public float timeToMove = 2.0f; // The amount of time it will take for the object to move
    public float maxHeight = -100f; // the maximum height before game over

    private bool isGameOver = false; // flag to prevent game over message from appearing multiple times
    private float distanceToMove; // The distance the object needs to move
    private bool isClicked = false; // A flag to indicate if the object has been clicked

    public static int bestRecord = 0;   // variable to store the best record
    public TextMeshProUGUI bestRecordText; 
    public ScoreManager scoreManager; //everything connected with score,i.e. resetting, incremetning,getting...
    public Animator animator;

    void Start()
    {
        Time.timeScale=1; //reseting the time after the reloading of the scene
        // Calculate the distance the object needs to move
        distanceToMove = transform.position.y + speed * timeToMove;
        transform.localScale = new Vector3(roundedScale(), roundedScale(), 1);
        bestRecord = PlayerPrefs.GetInt("BestRecord", 0);
        bestRecordText.text = "Best Record: " + bestRecord.ToString();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {   

        if(isGameOver){
            gameOverScreen.Setup();
            Time.timeScale=0;
        }
        animator.SetBool("isClicked",isClicked);
        // Move the object upward by the speed * deltaTime
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        //stex ban petq che avelacnel
        // Check if the object has moved the required distance
        if (transform.position.y >= distanceToMove)
        {
            // Stop moving the object
            enabled = false;
            
        }//
        // Check if the object has been clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Convert the mouse position to world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over the object
            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                isClicked=true;
                StartCoroutine(DestroyAfterAnimation(animator.GetCurrentAnimatorStateInfo(0).length));
                
                
            }
        }
  }

  

    public float scaleCalculator(float screenWidth)
    {
         //concatination for balloon for more correct adjusting
        float fifteenPercent = (screenWidth*15)/100; 
        float screenWidthMin = -Math.Abs(screenWidth - fifteenPercent);
        float screenWidthMax = screenWidth - fifteenPercent;
        float length = Math.Abs(screenWidthMax) + Math.Abs(screenWidthMin);
        float scale = length * 0.3f;    //taking the 30% is the proportion of the width of the screen to the balloon scale
        float rounded = Mathf.Round(scale * 10.0f) / 10.0f;
        return rounded;
    }

    public float roundedScale(){
        return scaleCalculator(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x * 2f);
    }
      IEnumerator DestroyAfterAnimation(float time)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(time);
        // Delete the object
        Destroy(gameObject);
        scoreManager.IncrementScore(); //equivalent to score++
 
        if (scoreManager.GetScore() > bestRecord)
        {
            // update the best record and save it to PlayerPrefs
            bestRecord = scoreManager.GetScore();
            PlayerPrefs.SetInt("BestRecord", bestRecord);
            bestRecordText.text = "Best Record: " + bestRecord.ToString();
        }
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Ceiling"))
        {
        if (!isGameOver && transform.position.y > maxHeight -10f)
            {
                isGameOver = true;
                scoreManager.ResetScore(); //reseting the score to 0 after we lose
            }
        }   
    }

    public void IncreaseSpeed(float increaseAmount)
    {
        float maxSpeed= 5.0f;
        if(speed<=maxSpeed)
        {
        speed = speed +increaseAmount;
        }

    }
    public float getSpeed()
    {
        return speed;
    }
}

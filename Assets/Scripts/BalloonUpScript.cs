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
    public TextMeshProUGUI scoreText; // text which shows the score
    private bool isGameOver = false; // flag to prevent game over message from appearing multiple times
    private float distanceToMove; // The distance the object needs to move
    private bool isClicked = false; // A flag to indicate if the object has been clicked
    private static int score=0; // counter of the number of balloons popped
     
    public Animator animator;



    void Start()
    {
        Time.timeScale=1; //reseting the time after the reloading of the scene
        // Calculate the distance the object needs to move
        distanceToMove = transform.position.y + speed * timeToMove;
        transform.localScale = new Vector3(roundedScale(), roundedScale(), 1);
         
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
        Debug.Log("scrMax" +screenWidthMax);
        Debug.Log("scrMin" +screenWidthMin);
        float length = Math.Abs(screenWidthMax) + Math.Abs(screenWidthMin);
        Debug.Log(length + "length " );
        float scale = length * 0.3f;    //taking the 30% is the proportion of the width of the screen to the balloon scale
        float rounded = Mathf.Round(scale * 10.0f) / 10.0f;
        Debug.Log("scale=" +rounded);
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
        score=score+1;  
        scoreText.text = "Score: " + score.ToString();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Ceiling"))
        {
        if (!isGameOver && transform.position.y > maxHeight -10f)
            {
                Debug.Log("Game over!");
                isGameOver = true;
                ResetScore(); //reseting the score to 0 after we lose
            }
        }   
    }
    void ResetScore() //just a method to reset the score to 0
    {
        score = 0;
    }
    public void IncreaseSpeed(float increaseAmount)
    {
        float maxSpeed= 5.0f;
        if(speed<=maxSpeed)
        {
        speed = speed +increaseAmount;
        Debug.Log(speed);
        }

    }
    public float getSpeed()
    {
        return speed;
    }
}

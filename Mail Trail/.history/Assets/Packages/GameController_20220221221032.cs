using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
      public static GameController instance;

      public GameObject boxContainer, hudContainer, gameOverPanel;
      public Text boxCounter, timeCounter, countdownText;
      public bool gamePlaying { get; private set; }
      public int countdownTime;

      private int numTotalBoxes, numCollectedBoxes;
      private float startTime, elapsedTime;
      TimeSpan timePlaying; 


      private void Awake()
      {
            instance = this; 
      }

      void Start () {
           numTotalBoxes = boxContainer.transform.childCount;
           numCollectedBoxes = 0;
           boxCounter.text = "Packages Collected: 0 / " + numTotalBoxes;

           gamePlaying = false; 

           StartCoroutine(CountdownToStart());
      }

      private void BeginGame()
      {
            gamePlaying = true; 
            startTime = Time.time + 60;
      }

      private void Update()
      {
            if (gamePlaying)
            {
                  elapsedTime = Time.time - startTime;
                  timePlaying = TimeSpan.FromSeconds(elapsedTime);

                  string timePlayingStr = "Time Left: " + timePlaying.ToString("mm':'ss'.'ff");
                  timeCounter.text = timePlayingStr;
            }
      }
      // Increases the score count each time a box is collected
      // Called in the player's Movement.cs file
      public void CollectBox()
      {
            numCollectedBoxes++;
            string boxCounterStr = "Packages: " + numCollectedBoxes + " / " + numTotalBoxes;
            boxCounter.text = boxCounterStr;

            if (numCollectedBoxes >= numTotalBoxes)
            {
                  EndGame();
            }
      }


      private void EndGame()
      {
            gamePlaying = false;
            Invoke("ShowGameOverScreen", 1.25f); //calls game over screen after 1.25 seconds 
      }

      private void ShowGameOverScreen()
      {
            gameOverPanel.SetActive(true);
            hudContainer.SetActive(false);
            string timePlayingStr = "Time Left: " + timePlaying.ToString("mm':'ss'.'ff");
            gameOverPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayingStr;
      }

      IEnumerator CountdownToStart()
      {
            while(countdownTime > 0) 
            {
                  countdownText.text = countdownTime.ToString();
                  yield return new WaitForSeconds(1f);
                  countdownTime--;
            }

            countdownText.text = "GO!";

            // GameController.instance.BeginGame();

            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false); 
      }


      // public void AddScore (int newScoreValue) {
      //       score += newScoreValue;
      //       UpdateScore ();
      //       }

      // void UpdateScore () {
      //       Text scoreTextB = textGameObject.GetComponent<Text>();
      //        scoreTextB.text = "Score: " + score;
      //       }
}

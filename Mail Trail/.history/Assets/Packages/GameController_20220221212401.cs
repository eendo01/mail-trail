using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
      public static GameController instance;

      public GameObject boxContainer, hudContainer, gameOverPanel;
      public Text boxCounter, timeCounter;
      public bool gamePlaying { get; private set; }

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

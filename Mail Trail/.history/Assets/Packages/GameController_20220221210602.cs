using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
      public static GameController instance;

      public GameObject boxContainer, hudContainer;
      public Text boxCounter, timeCounter;

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

      public void CollectBox()
      {
            numCollectedBoxes++;
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

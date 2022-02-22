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

      private int score; 

      private float startTime, elapsedTime;
      TimeSpan timePlaying; 


      private void Awake()
      {
            instance = this; 
      }

      void Start () {
           score = 0;
           UpdateScore();
            }

      public void AddScore (int newScoreValue) {
            score += newScoreValue;
            UpdateScore ();
            }

      void UpdateScore () {
            Text scoreTextB = textGameObject.GetComponent<Text>();
             scoreTextB.text = "Score: " + score;
            }
}

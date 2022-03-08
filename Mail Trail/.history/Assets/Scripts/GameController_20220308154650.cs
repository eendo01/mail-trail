using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
      public static GameController instance;
      
      public static bool GameisPaused = false;
      public GameObject pauseMenuUI;
      
      // public GameObject popUpBox;
      // public Animator animator;
      // public Text popUPText;
      
      public GameObject boxContainer, hudContainer, gameSuccessPanel, gameOverPanel;
      public Text boxCounter, timeCounter, countdownText, PauseMenuText;
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
           pauseMenuUI.SetActive(false);

           GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

           StartCoroutine(CountdownToStart());
      }

      private void BeginGame()
      {
            gamePlaying = true; 
            startTime = Time.time + 5;
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
      }

      private void Update()
      {
            if (gamePlaying)
            {
                  elapsedTime = Time.time - startTime;
                  timePlaying = TimeSpan.FromSeconds(elapsedTime);

                  string timePlayingStr = "Time Left: " + timePlaying.ToString("mm':'ss'.'ff");
                  timeCounter.text = timePlayingStr;

                  if (timePlaying.ToString("mm':'ss'.'ff") == "00:00.00")
                  {
                        EndGameFailure();
                  }
            }
            if (Input.GetKeyDown(KeyCode.Escape)){
                    if (GameisPaused){
                            Resume();
                    }
                    else{
                            Pause();
                    }
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
                  EndGameSuccess();
            }
      }


      private void EndGameSuccess()
      {
            gamePlaying = false;
            Invoke("ShowGameSuccessScreen", 1.25f); //calls game over screen after 1.25 seconds 
      }

      private void EndGameFailure()
      {
            gamePlaying = false;
            Invoke("ShowGameOverScreen", 1.25f); //calls game over screen after 1.25 seconds 
      }

      private void ShowGameSuccessScreen()
      {
            gameSuccessPanel.SetActive(true);
            hudContainer.SetActive(false);
            string timePlayingStr = "Time Left: " + timePlaying.ToString("mm':'ss'.'ff");
            gameSuccessPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayingStr;
      }

      private void ShowGameOverScreen()
      {
            gameOverPanel.SetActive(true);
            hudContainer.SetActive(false);
            string timePlayingStr = "Time Left: " + timePlaying.ToString("mm':'ss'.'ff");
            gameOverPanel.transform.Find("FinalTimeText").GetComponent<Text>().text = timePlayingStr;

            Invoke("Start", 1.25f);
            
      }
      
      void Pause(){
              pauseMenuUI.SetActive(true);
              Time.timeScale = 0f;
              GameisPaused = true;
              //PauseMenuText.text = "PAUSED";
      }
      
      public void Resume(){
              pauseMenuUI.SetActive(false);
              Time.timeScale = 1f;
              GameisPaused = false;
      }
      
      public void RestartGame(){
              Time.timeScale = 1f;
              SceneManager.LoadScene("WorldMap");
      }

      public void QuitGame(){
              #if UNITY_EDITOR
              UnityEditor.EditorApplication.isPlaying = false;
              #else
              Application.Quit();
              #endif
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

            GameController.instance.BeginGame();

            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false); 
      }

      // POPUP code
      // public void PopUp(string Text)
      // {
      //     popUpBox.SetActive(true);
      //     popUpText.text = text;
      //     animator.SetTrigger("pop");
      // }
      
      // public void AddScore (int newScoreValue) {
      //       score += newScoreValue;
      //       UpdateScore ();
      //       }

      // void UpdateScore () {
      //       Text scoreTextB = textGameObject.GetComponent<Text>();
      //        scoreTextB.text = "Score: " + score;
      //       }
}

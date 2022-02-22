using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
       private GameController gameController; // #1: add variable to hold GameController 

       void Start () {                                           // #2: assign actual Game Controller to variable
          if (GameObject.FindWithTag ("GameController") != null) { gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
         }
      }

       void OnTriggerEnter(Collider other){
             if (other.gameObject.tag == "Player") { 
                   GetComponent<Collider>().enabled = false; 
                   GetComponent<AudioSource>().Play();
                   gameController.AddScore (1);         // #3: update score through AddScore()
                   StartCoroutine(DestroyThis());
             }
       }

       IEnumerator DestroyThis(){
             yield return new WaitForSeconds(0.5f);
             Destroy(gameObject);
       }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    // option to start game
   public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.tag == "Player")
    	{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    	}
     }
}
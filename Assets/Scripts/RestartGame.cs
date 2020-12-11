using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

    // option to start game
   public void RestartGameTrigger ()
    {
        SceneManager.LoadScene(0);
    }

} 
        


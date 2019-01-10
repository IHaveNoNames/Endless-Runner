using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    
	

    public void LoadGameLevel()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    
}

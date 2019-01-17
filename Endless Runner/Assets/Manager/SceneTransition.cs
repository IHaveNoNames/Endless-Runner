using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    
	

    public void LoadGameLevel()
    {
        
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
    public void UnloadGameLevel()
    {
        SceneManager.UnloadSceneAsync("SampleScene");
    }

    public void UnloadMenuLevel()
    {
        SceneManager.UnloadSceneAsync(0);
    }

    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

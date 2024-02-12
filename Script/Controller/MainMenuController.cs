using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void ScoreButton()
    {
        SceneManager.LoadScene("GameScore");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}

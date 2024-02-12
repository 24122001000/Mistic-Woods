using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject defeatBossPanel;
    private void Update()
    {
        MakeInstance();
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Door()
    {
        SceneManager.LoadScene("BossRoom");
    }
    public void ActiveCanvas()
    {
        gameOverPanel.SetActive(true);
    }
    public void BossDefeat()
    {
        defeatBossPanel.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
}

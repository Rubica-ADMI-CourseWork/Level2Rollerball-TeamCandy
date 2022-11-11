using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Game Panels Variables")]
    public GameObject victoryScreen;
    public GameObject pausePanel;

    bool isPaused = false;


    [Header("Score Variables")]
    public TMP_Text scoreText;
    public TMP_Text scoreVictoryScreenText;

    [Header("Ammo Variables")]
    public TMP_Text stickyAmmoText;
    public TMP_Text popRockAmmoText;
    public TMP_Text sourSplashAmmoText;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        victoryScreen.SetActive(false);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {

        Application.Quit();
    }

    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ReplayButton()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void HighScoreButton()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void MuteToggle(bool isMuted)
    {
        if (isMuted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void PlayGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGameButton()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}

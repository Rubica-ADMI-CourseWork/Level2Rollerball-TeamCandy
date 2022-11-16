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
    public GameObject finalVictoryScreen;

    bool isPaused = false;


    [Header("Score Variables")]
    public TMP_Text scoreText;
    public TMP_Text scoreVictoryScreenText;

    int sessionNumber; //Variable to store Game Session Number, which updates everytime a new game is played, each dice roll is a session in this case
   
    PCController pcController;

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

        //Get session number integer value from key "Session Number", if key not found create key with value = 1
        sessionNumber = PlayerPrefs.GetInt("SessionNumber", 1);

        Debug.Log("Current session number is:" + sessionNumber);

        pcController = GameObject.Find("PC").GetComponent<PCController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        victoryScreen.SetActive(false);
        pausePanel.SetActive(false);
        finalVictoryScreen.SetActive(false);
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


    //public void ReplayButton()
    //{
    //    Time.timeScale = 1f;
    //    Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    //}

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

    public void LevelsSelectionButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelection");
    }

    public void ChooseLevel1Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void ChooseLevel2Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    public void ChooseLevel3Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    }

    public void ChooseLevel4Button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level4");
    }

    public void VictoryScreenReplayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseReplayButton()
    {        
        int scoreNumber = pcController.score;
                
        SendScoreToManager(scoreNumber, sessionNumber);

        sessionNumber += 1;

        //Update the session number to whatever the current session number is, in the save data
        PlayerPrefs.SetInt("SessionNumber", sessionNumber);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SendScoreToManager(int score, int sessionNumber)
    {
        ScoreManager.instance.AddNewScore(score, sessionNumber);
    }
}

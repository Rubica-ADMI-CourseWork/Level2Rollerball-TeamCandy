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

    [SerializeField]
    bool isPaused;

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
        pausePanel.SetActive(false);

        if(victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }

        if(finalVictoryScreen != null)
        {
            finalVictoryScreen.SetActive(false);
        }

        isPaused = false;
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
        
        if (isPaused == false)
        {
            Debug.Log("Inside PauseGameButton(): Pausing..");

            pausePanel.SetActive(true);
            TimeManager.Instance.PauseGame(true);
            isPaused = true;
        }
        else if(isPaused == true)
        {
            Debug.Log("Inside PauseGameButton(): UnPausing..");

            pausePanel.SetActive(false);
            TimeManager.Instance.PauseGame(false);
            isPaused = false;
        }
    }

    public void LevelsSelectionButton()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene("LevelSelection");
    }

    public void ChooseLevel1Button()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene("Level1Explanation");
    }

    public void ChooseLevel2Button()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene("Level2Explanation");
    }

    public void ChooseLevel3Button()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene("Level3Explanation");
    }

    public void ChooseLevel4Button()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene("Level4Explanation");
    }

    public void VictoryScreenReplayButton()
    {
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SkipButton()
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void PauseReplayButton()
    {        
        int scoreNumber = pcController.score;
                
        SendScoreToManager(scoreNumber, sessionNumber);

        sessionNumber += 1;

        //Update the session number to whatever the current session number is, in the save data
        PlayerPrefs.SetInt("SessionNumber", sessionNumber);
        TimeManager.Instance.PauseGame(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SendScoreToManager(int score, int sessionNumber)
    {
        ScoreManager.instance.AddNewScore(score, sessionNumber);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class CandyGameManager : MonoBehaviour
{
    public static CandyGameManager instance;

    public enum levelConditionStates
    {
        Level1,
        Level2,
        Level3,
        Level4
    }

    public levelConditionStates currentLevelConditionState;

    [Header("Level 1 Variables")]
    public GameObject level1Panel;

    public int currentNumberOfCheckpointsPassed;
    public int totalnumberOfCheckpoints;

    public TMP_Text checkpointsText;

    [Header("Level 2 Variables")]
    public GameObject level2Panel;

    public int currentNumberOfRawMaterials;
    public int totalnumberOfRawMaterials;

    public TMP_Text rawMaterialsText;
    public TMP_Text timerText;

    public float currentTime;

    [Header("Level 3 Variables")]
    public GameObject level3Panel;
    public GameObject lv3strikeThrough;
    public GameObject lv3newObjective;
    public GameObject lv3TeleportationPanel;
    public GameObject lv3TeleportationButton;
    public GameObject lv3PanelCloseButton;

    public int currentNumberOfPickups;
    public int totalNumberOfPickups;
    public int extraPickups;

    public TMP_Text pickupText;

    bool closingTeleportationPanel = false;

    [Header("Level 4 Variables")]
    public GameObject level4Panel;

    public int currentNumberOfEnemies;
    public int totalNumberOfEnemies;
    public int extraEnemies;

    public TMP_Text enemiesText;

    
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
        //Screen doesn't turn off during gameplay
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        level1Panel.SetActive(false);

        level2Panel.SetActive(false);

        level3Panel.SetActive(false);
        lv3strikeThrough.SetActive(false);
        lv3newObjective.SetActive(false);
        lv3TeleportationPanel.SetActive(false);
        lv3PanelCloseButton.SetActive(false);
        lv3TeleportationButton.SetActive(false);
        

        level4Panel.SetActive(false);

        //Level 1 stuff
        totalnumberOfCheckpoints = GameObject.FindGameObjectsWithTag("CheckpointObjective").Length; //Get the total number of checkpoints in the scene

        //Level 2 stuff
        totalnumberOfRawMaterials = 15;

        //Level 3 stuff
        totalNumberOfPickups = 15;
        
        //Level 4 stuff
        totalNumberOfEnemies = 10;

    }

    // Update is called once per frame
    void Update()
    {
        CheckTheScenes();

        switch (currentLevelConditionState)
        {
            case levelConditionStates.Level1:

                if (currentLevelConditionState != levelConditionStates.Level1)
                {
                    currentLevelConditionState = levelConditionStates.Level1;
                }

                level1Panel.SetActive(true);

                checkpointsText.text = $"{currentNumberOfCheckpointsPassed} / {totalnumberOfCheckpoints}"; //What the UI text displays

                break;

            case levelConditionStates.Level2:

                if (currentLevelConditionState != levelConditionStates.Level2)
                {
                    currentLevelConditionState = levelConditionStates.Level2;
                }

                level2Panel.SetActive(true);

                rawMaterialsText.text = $"{currentNumberOfRawMaterials} / {totalnumberOfRawMaterials}"; //What the UI text displays

                //Timer stuff
                currentTime += Time.deltaTime; //set the time to start counting when the scene loads

                float minutes = Mathf.FloorToInt(currentTime / 60); //convert to minutes
                float seconds = Mathf.FloorToInt(currentTime % 60); //convert to seconds

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //how it will be seen on the UI
                                
                break;

            case levelConditionStates.Level3:

                if (currentLevelConditionState != levelConditionStates.Level3)
                {
                    currentLevelConditionState = levelConditionStates.Level3;
                }

                level3Panel.SetActive(true);

                pickupText.text = $"{currentNumberOfPickups} / {totalNumberOfPickups}"; //What the UI text displays

                if (currentNumberOfPickups == totalNumberOfPickups)
                {
                    Time.timeScale = 0f;

                    lv3strikeThrough.SetActive(true);
                    lv3newObjective.SetActive(true);
                    lv3TeleportationPanel.SetActive(true);
                    lv3PanelCloseButton.SetActive(true);
                    lv3TeleportationButton.SetActive(true);
                }
                if (closingTeleportationPanel == true)
                {
                    lv3TeleportationPanel.SetActive(false);
                    lv3PanelCloseButton.SetActive(false);

                    Time.timeScale = 1f;
                }

                extraPickups = currentNumberOfPickups - totalNumberOfPickups; //get the extra pickups and give bonuses accordingly

                break;

            case levelConditionStates.Level4:

                if (currentLevelConditionState != levelConditionStates.Level4)
                {
                    currentLevelConditionState = levelConditionStates.Level4;
                }

                level4Panel.SetActive(true);

                enemiesText.text = $"{currentNumberOfEnemies} / {totalNumberOfEnemies}"; //What the UI text displays

                extraEnemies = currentNumberOfEnemies - totalNumberOfEnemies; //get the extra enemies killed and give bonuses accordingly

                break;
        }
    }

    void CheckTheScenes()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1"))
        {
            if (currentLevelConditionState != levelConditionStates.Level1)
            {
                currentLevelConditionState = levelConditionStates.Level1;
            }
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2"))
        {
            if (currentLevelConditionState != levelConditionStates.Level2)
            {
                currentLevelConditionState = levelConditionStates.Level2;
            }
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            if (currentLevelConditionState != levelConditionStates.Level3)
            {
                currentLevelConditionState = levelConditionStates.Level3;
            }
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level4"))
        {
            if (currentLevelConditionState != levelConditionStates.Level4)
            {
                currentLevelConditionState = levelConditionStates.Level4;
            }
        }

    }

    public void CloseTeleportationPanelButton()
    {        
        closingTeleportationPanel = true;       
    }

}

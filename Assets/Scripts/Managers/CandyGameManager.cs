using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    [Header("Level 2 Variables")]
    public GameObject level2Panel;


    [Header("Level 3 Variables")]
    public GameObject level3Panel;


    [Header("Level 4 Variables")]
    public GameObject level4Panel;


    
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
      level1Panel.SetActive(false);
      level2Panel.SetActive(false);
      level3Panel.SetActive(false);
      level4Panel.SetActive(false);

        //Level 1 stuff
        totalnumberOfCheckpoints = 3;

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

                break;

            case levelConditionStates.Level2:

                if (currentLevelConditionState != levelConditionStates.Level2)
                {
                    currentLevelConditionState = levelConditionStates.Level2;
                }

                level2Panel.SetActive(true);

                break;

            case levelConditionStates.Level3:

                if (currentLevelConditionState != levelConditionStates.Level3)
                {
                    currentLevelConditionState = levelConditionStates.Level3;
                }

                level3Panel.SetActive(true);

                break;

            case levelConditionStates.Level4:

                if (currentLevelConditionState != levelConditionStates.Level4)
                {
                    currentLevelConditionState = levelConditionStates.Level4;
                }

                level4Panel.SetActive(true);

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

   
}

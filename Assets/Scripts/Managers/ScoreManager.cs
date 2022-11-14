using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreUIPrefab;
    public GameObject leaderboardPanel;

    public static ScoreManager instance;

    public List<GameObject> spawnedScoreUIPrefabs;

    private void Awake()
    {
        //Initialize the instance variable to create a singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        //Load saved data
        string savedData = PlayerPrefs.GetString("highScoreTable");

        //Convert string value to HighScore data type
        ScoreList scoreList = JsonUtility.FromJson<ScoreList>(savedData);

        //if there is no highscore when game starts, populate some dummy values and reload save data
        if (scoreList == null)
        {
            AddNewScore(0, 0);
            AddNewScore(0, 0);
            AddNewScore(0, 0);
            AddNewScore(0, 0);
            AddNewScore(0, 0);

            //Reload Data
            //Load saved data
            savedData = PlayerPrefs.GetString("highScoreTable");

            //Convert string value to HighScore data type
            scoreList = JsonUtility.FromJson<ScoreList>(savedData);
        }
        //Arrange the list in descending order by score, and if score is equal then by session number +++++++++++++++++++++++++++++++++++

        //now we sort the data according to descending order of score and if score is equal, then descending order of session number
        for (int i = 0; i < scoreList.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < scoreList.highScoreEntryList.Count; j++)
            {
                if (scoreList.highScoreEntryList[j].score > scoreList.highScoreEntryList[i].score)
                {
                    ScoreEntry temp = scoreList.highScoreEntryList[i];
                    scoreList.highScoreEntryList[i] = scoreList.highScoreEntryList[j];
                    scoreList.highScoreEntryList[j] = temp;
                }
                else if (scoreList.highScoreEntryList[j].score == scoreList.highScoreEntryList[i].score)
                {
                    if (scoreList.highScoreEntryList[j].sessionNumber > scoreList.highScoreEntryList[i].sessionNumber)
                    {
                        ScoreEntry temp = scoreList.highScoreEntryList[i];
                        scoreList.highScoreEntryList[i] = scoreList.highScoreEntryList[j];
                        scoreList.highScoreEntryList[j] = temp;
                    }
                }
            }
        }

        //Spawn the Score UI Prefab for the associated high score entries +++++++++++++++++++++++++++++++++++++++

        //Go through each entry in the list we retrieved from save data
        foreach (ScoreEntry scoreEntry in scoreList.highScoreEntryList)
        {
            if(leaderboardPanel != null)
            {

                //Instantiate the Score UI Prefab
                GameObject scorePrefab = Instantiate(scoreUIPrefab, leaderboardPanel.transform);

                //Get Access to HighScorePrefab Script attached to the Score UI Prefab and set the score UI text to the associated score
                scorePrefab.GetComponent<ScorePrefab>().scoreUIText.text = scoreEntry.score.ToString();

                //Get Access to HighScorePrefab Script attached to the Score UI Prefab and set the session number UI text to the associated session number
                scorePrefab.GetComponent<ScorePrefab>().sessionNumberUIText.text = scoreEntry.sessionNumber.ToString();

                spawnedScoreUIPrefabs.Add(scorePrefab);
            }
        }

    }

    
    // Update is called once per frame
    void Update()
    {
        ClearPrefabList();
    }

    public void AddNewScore(int score, int sessionNumber)
    {
        //Get previous data from disk so it can be updated +++++++++++++++++++++++++++++++++++++++++++++++++++++

        //create a new HighScoreEntry
        ScoreEntry scoreEntry = new ScoreEntry { score = score, sessionNumber = sessionNumber };

        //Load saved data
        string savedData = PlayerPrefs.GetString("highScoreTable");

        //Convert string value to HighScore data type
        ScoreList scoreList = JsonUtility.FromJson<ScoreList>(savedData);

        //Previous data gathered +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //if there is no data to read, we initialize the list and create a new one
        if (scoreList == null)
        {
            scoreList = new ScoreList() { highScoreEntryList = new List<ScoreEntry>() };
        }

        //Add new entry to the HighScoreList
        scoreList.highScoreEntryList.Add(scoreEntry);

        //Save updated highscores ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //Convert list to json string
        string dataToBeSaved = JsonUtility.ToJson(scoreList);

        //Save json string using Player Prefs
        PlayerPrefs.SetString("highScoreTable", dataToBeSaved);

        //Hard Save it to disk
        PlayerPrefs.Save();
    }

    public void DeleteSaveFile()
    {
        PlayerPrefs.DeleteKey("highScoreTable");
        PlayerPrefs.DeleteKey("SessionNumber");
    }

    void ClearPrefabList()
    {
        if (spawnedScoreUIPrefabs.Count > 4)
        {
            for (int i = spawnedScoreUIPrefabs.Count - 1; i >= 4; i--)
            {
                Destroy(spawnedScoreUIPrefabs[i].gameObject);
            }

            spawnedScoreUIPrefabs.RemoveRange(4, spawnedScoreUIPrefabs.Count - 4);

        }
    }

}

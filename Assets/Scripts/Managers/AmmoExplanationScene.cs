using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AmmoExplanationScene : MonoBehaviour
{
    public float waitTime = 7f;
    public void SkipButton()
    {
       LoadNextScene();
    }

    void LoadNextScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void Awake()
    {
        StartCoroutine(LoadScreenTime());
    }
    IEnumerator LoadScreenTime()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool isGamePaused;
    //private backing field to store the TimeManager Instance
    private static TimeManager _instance;

    //public property to allow access to the instance of Timemanger
    public static TimeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TimeManager>();
                if(_instance == null)
                {
                    _instance = new GameObject().AddComponent<TimeManager>();
                }
            }
            return _instance; 
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Pauses the time based on the value passed in
    /// if _value is true: the game paused, else if _value is false: the game un pauses
    /// </summary>
    /// <param name="_value"></param>
    public void PauseGame(bool _value)
    {
        if(_value == true)
        {
        Time.timeScale = 0;
            isGamePaused = true;
        }
        if(_value == false)
        {
            Time.timeScale = 1;
            isGamePaused=false;
        }
    }
}

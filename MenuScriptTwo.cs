﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScriptTwo : MonoBehaviour
{


    public void PlayGame()
    {

        SceneManager.LoadScene(1);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

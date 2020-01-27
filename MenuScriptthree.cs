using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScriptthree : MonoBehaviour
{


    public void PlayGame()
    {

        SceneManager.LoadScene("firstlevel");

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("secondlevel");
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

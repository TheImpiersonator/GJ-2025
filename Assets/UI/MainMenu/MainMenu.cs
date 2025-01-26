using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Controls()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

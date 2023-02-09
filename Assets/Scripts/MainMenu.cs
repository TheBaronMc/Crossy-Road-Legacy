using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const int GAME_SCENE = 1;

    public void PlayGame()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

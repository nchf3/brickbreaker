using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public void restart_game()
    {
        Debug.Log("Restarting the game");

        SceneManager.LoadScene("Scenes/main_scene");
    }

    public void stop_game()
    {
        Debug.Log("Stop the game");

        Application.Quit();
    }
}

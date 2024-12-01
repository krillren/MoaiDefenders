using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GetSelectionMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/LevelSelectionMenu.unity");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting here : main menu");
        Application.Quit();
    }
}

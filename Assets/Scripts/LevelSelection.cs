using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour
{
    public void getMainMenu()
    {
        SceneManager.LoadScene("Assets/Scenes/MainMenu.unity");
    }
}
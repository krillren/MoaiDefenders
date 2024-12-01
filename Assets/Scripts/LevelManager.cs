using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void getLevel(int level_id)
    {
        string level_name = "Assets/Scenes/Level" + level_id + ".unity";
    }
}

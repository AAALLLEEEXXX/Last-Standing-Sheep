using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static GameMenu gameMenu;
    public GameObject failScreen;
    public GameObject winScreen;

    public void ShowMenu(bool deadPlayer)
    {
        if (deadPlayer)
            failScreen.SetActive(true);
        else
            winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}

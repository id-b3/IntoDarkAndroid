using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void PlayDemo()
    {
        SceneManager.LoadScene("DemoRoom");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

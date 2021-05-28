using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject helpPanel;
    public Text helpText;

    private void Awake()
    {
        Screen.SetResolution(1980, 1080, FullScreenMode.MaximizedWindow, 144);
    }

    private void Start()
    {
        helpText.text = OnClickHelp();
    }

    public void OnClickStartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void OnClickHelpButton()
    {
        helpPanel.SetActive(true);
    }
    
    public string OnClickHelp()
    {
        return
            "* You can move WASD or arrow keys\nAiming by Mouse\n" +
            "* Press K -> stop bgm\nPress L -> play bgm\n" +
            "* Survive from zombie and Protect your core as longer as you can\n" +
            "* When you start a game, you can't get hp and ammo\n";
    }

    public void ExitProgram()
    {
        Application.Quit();
    }
    
    public void UnActiveHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public TMP_Text saveErrorText;
    public TextPopupFade fader;

    private void Start()
    {
        SaveSystem.ClearPersistentData();
    }

    public void LoadNew()
    {
        PlayerPrefs.SetInt("LoadFromScene", -1);
        PlayerPrefs.SetInt("Timer", 600);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }
    
    public void LoadSave()
    {
        if (SaveSystem.CheckIfSaveExists())
        {
            PlayerPrefs.SetInt("LoadFromScene", 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Hub", LoadSceneMode.Single);
        }
        else
        {
            fader.CreatePopup(saveErrorText);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

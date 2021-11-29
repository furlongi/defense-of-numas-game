using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public TMP_Text saveErrorText;
    public TextPopupFade fader;

    public void LoadNew()
    {
        PlayerPrefs.SetInt("LoadFromScene", -1);
        SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }
    
    public void LoadSave()
    {
        if (SaveSystem.CheckIfSaveExists())
        {
            PlayerPrefs.SetInt("LoadFromScene", 0);
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

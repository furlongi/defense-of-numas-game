using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipMenu : MonoBehaviour
{
    public TMP_Text saveNotice1;
    public TMP_Text saveNotice2;
    public TextPopupFade fader;
    
    public void SaveGame()
    {
        GameObject Pobj = GameObject.Find("Player");
        Player _player = Pobj.GetComponent<Player>();
        
        saveNotice1.gameObject.SetActive(true);
        SaveSystem.SavePlayer(_player);
        saveNotice1.gameObject.SetActive(false);
        fader.CreatePopup(saveNotice2);
        PlayerPrefs.SetInt("IsNewGame", 0);
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Start");
    }
}

using System.Collections;
using TMPro;
using UnityEngine;


public class GameOverHandler : MonoBehaviour
{
    public SceneLoader loader;
    public PlayerMovement player;
    public TextPopupFade popup;
    public TMP_Text gameOverText;

    public void HandleGameOver()
    {
        StartCoroutine(WrapUpGameOver());
    }
    
    IEnumerator WrapUpGameOver()
    {
        player.OccupyPlayer();
        popup.CreatePopup(gameOverText);
        yield return new WaitForSeconds(5f); // Time to wait until load save
        loader.LoadGameOver();
    }
}

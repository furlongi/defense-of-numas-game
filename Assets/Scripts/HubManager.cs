using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public GameObject mineTeleporter;
    public GameObject tdTeleporter;
    public GameObject mineCollider;
    public Player player;
    public Transform promptExitToTD;
    public Transform exitFallback;
    
    
    private void Start()
    {
        int timer = PlayerPrefs.GetInt("Timer", 600);
        int waveNum = PlayerPrefs.GetInt("Wave", 1);
        if (timer <= 0)
        {
            mineTeleporter.SetActive(false);
            mineCollider.SetActive(true);
        }

        if (waveNum > 3)
        {
            tdTeleporter.SetActive(false);
        }
    }

    public void ShouldExitPrompt()
    {
        player.GetComponent<PlayerMovement>().OccupyPlayer();
        promptExitToTD.gameObject.SetActive(true);
    }

    public void ConfirmExit(bool answer)
    {
        if (answer)
        {
            sceneLoader.LoadScene("TowerDefense", "Hub");
        }
        else
        {
            promptExitToTD.gameObject.SetActive(false);
            player.transform.position =
                new Vector3(exitFallback.position.x, exitFallback.position.y, player.transform.position.z);
            player.GetComponent<PlayerMovement>().FreePlayer();
        }
    }
    
}

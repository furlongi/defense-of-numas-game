using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public string sceneName;
    public string sceneOrigin;
    public TowerList towerList;
    public Player player;
    public EventManager manager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerPart"))
        {
            SaveSystem.SavePersistentTower(towerList);
            PlayerPrefs.SetInt("Timer", 600);
            PlayerPrefs.SetInt("Lives", manager.currentPopulation);
            PlayerPrefs.Save();
            player.HealMax();
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
            sceneLoader.LoadScene(sceneName, sceneOrigin);
        }
    }
}
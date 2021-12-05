using UnityEngine;

public class SceneExit : MonoBehaviour
{
    public string sceneName;
    public string sceneOrigin;
    public TowerList towerList;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerPart"))
        {
            SaveSystem.SavePersistentTower(towerList);
            PlayerPrefs.SetInt("Timer", 600);
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("HealthCap", 20));
            PlayerPrefs.Save();
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
            sceneLoader.LoadScene(sceneName, sceneOrigin);
        }
    }
}
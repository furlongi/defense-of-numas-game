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
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
            sceneLoader.LoadScene(sceneName, sceneOrigin);
        }
    }
}
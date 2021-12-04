using UnityEngine;

public class MineSceneTeleport : MonoBehaviour
{
    public string sceneName;
    public string sceneOrigin;
    public MineManager manager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerPart"))
        {
            manager.StopTimer();
            manager.StoreTimer();
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
            sceneLoader.LoadScene(sceneName, sceneOrigin);
        }
    }
}

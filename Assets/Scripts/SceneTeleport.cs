
using System;
using UnityEngine;

public class SceneTeleport : MonoBehaviour
{
    public string sceneName;
    public string sceneOrigin;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerPart"))
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
            sceneLoader.LoadScene(sceneName, sceneOrigin);
        }
    }
}

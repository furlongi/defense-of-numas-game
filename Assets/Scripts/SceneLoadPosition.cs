using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SceneLoadPosition : MonoBehaviour
{
    // Assign with inspector
    public List<SceneSpawns> positions;
    public Player player;

    public void MoveToPosition(string positionName)
    {
        foreach (var p in positions)
        {
            if (p.sceneName.Equals(positionName))
            {
                Vector3 vec = p.transform.position;
                vec.z = player.transform.position.z;
                player.transform.position = vec;
                break;
            }
        }
        // If none found, then the player is not moved and the default position
        // from the scene is used.
    }
}

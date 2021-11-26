using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void StartDeathTransition()
    {
        IEnumerator doDeath = DoDeath();
        StartCoroutine(doDeath);
    }

    private IEnumerator DoDeath()
    {
        yield return new WaitForSeconds(3f);
        sceneLoader.LoadDeath();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            MineManager manager = FindObjectOfType<MineManager>().GetComponent<MineManager>();
            manager.ShouldExitPrompt();
        }
    }
}

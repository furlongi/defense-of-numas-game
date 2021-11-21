using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            MineManager manager = GameObject.FindObjectOfType<MineManager>().GetComponent<MineManager>();
            manager.ChooseNextFloor();
        }
    }
}

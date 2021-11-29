using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopup : MonoBehaviour
{

    public GameObject menu; // Assign
    public GameObject menu2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            menu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerPart"))
        {
            menu.SetActive(false);
            menu2.SetActive(false);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{

    private bool _isOnTop = false;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        _player = obj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOnTop)
        {
            if (Input.GetKeyDown("f"))
            {
                Debug.Log("Saving Game.");
                SaveSystem.SavePlayer(_player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isOnTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isOnTop = false;
        }
    }

}

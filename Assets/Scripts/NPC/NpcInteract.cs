using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    public float talkDistance = 1f;
    public string npcName;
    public TextAsset npcText;

    // Start is called before the first frame update
    void Start()
    {
        if (npcText == null)
        {
            Debug.Log("Dialogue text for " + npcName + " is missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject player = GameObject.Find("Player");
        var distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= talkDistance)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(npcName, npcText);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    public float talkDistance = 1f;
    public Dialogue dialogue;
    public string npcName;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, npcName);
        }
    }
}

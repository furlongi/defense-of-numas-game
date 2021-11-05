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

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartInteraction();
        }
    }

    private void OnMouseDown()
    {
        StartInteraction();
    }

    private void StartInteraction()
    {
        GameObject player = GameObject.Find("Player");
        var distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance <= talkDistance)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(npcName, npcText);
        }
    }
}

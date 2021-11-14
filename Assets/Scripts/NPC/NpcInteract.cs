using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    public float talkDistance = 1f;
    public bool turnsWhenWalkNear = true;
    public bool isShop = false;
    
    // Set these in inspector
    public string npcName;
    public TextAsset npcText;
    
    private GameObject _player;
    private SpriteRenderer _sprite;

    void Start()
    {
        if (npcText == null && !isShop)
        {
            Debug.Log("Dialogue text for " + npcName + " is missing!");
        }
        _player = GameObject.Find("Player");
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (turnsWhenWalkNear && 
            Vector2.Distance(transform.position, _player.transform.position) <= talkDistance * 1.5)
        {
            TurnSprite(_player.transform.position.x, transform.position.x);
        }
        
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

        if (distance <= talkDistance)
        {
            TurnSprite(_player.transform.position.x, transform.position.x);
            PlayerMovement pm = _player.GetComponent<PlayerMovement>();
            if (isShop)
            {
                Shooting sh = _player.GetComponentInChildren<Shooting>();
                Player pl = _player.GetComponent<Player>();
                FindObjectOfType<WeaponShopManager>().StartShop(npcName, pm, sh, pl);
            }
            else
            {
                FindObjectOfType<DialogueManager>().StartDialogue(npcName, npcText, pm);
            }
        }
    }

    private void TurnSprite(float target, float origin)
    {
        float diff = target - origin;
        if (diff > 0)
        {
            _sprite.flipX = false;
        } 
        else if (diff < 0)
        {
            _sprite.flipX = true;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Assign these below to the appropriate objects
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    // -
    
    private Queue<string> _sentences;
    private string _curSentence;
    private IEnumerator _sentenceTyper = null;
    

    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(string npcName, TextAsset npcText)
    {
        NpcTemplate dialg = DialogueLoader.LoadXML(npcText);
        animator.SetBool("isOpen", true);
        _sentences.Clear();
        nameText.text = npcName;
        
        foreach (var sent in dialg.World.EventType.Text)
        {
            _sentences.Enqueue(sent.Trim());
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _curSentence = _sentences.Dequeue();
        if (_sentenceTyper != null) // If dialogue is still being typed
        {
            StopCoroutine(_sentenceTyper);
        }

        _sentenceTyper = TypeSentenceCoroutine(_curSentence);

        StartCoroutine(_sentenceTyper);
    }

    public void AccelerateSentence()
    {
        if (_sentenceTyper != null)
        {
            StopCoroutine(_sentenceTyper);
            dialogueText.text = _curSentence;
            _sentenceTyper = null;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    IEnumerator TypeSentenceCoroutine(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        _sentenceTyper = null;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
    
    private void Update() {
        if (Input.GetKeyDown("space")) {
            AccelerateSentence();
        }
    }
    
}

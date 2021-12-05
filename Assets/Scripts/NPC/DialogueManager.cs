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
    private PlayerMovement _pm;
    private bool _isBusy = false;
    

    void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(string npcName, TextAsset npcText, PlayerMovement pm)
    {
        if (npcText == null || _isBusy)
        {
            return;
        }
        
        NpcTemplate dialg = DialogueLoader.LoadXML(npcText);
        animator.SetBool("isOpen", true);
        _sentences.Clear();
        nameText.text = npcName;
        _pm = pm;

        foreach (var sent in EventFlagCheck(dialg.World.EventType).Text)
        {
            _sentences.Enqueue(sent.Trim());
        }
        
        _pm.OccupyPlayer();
        _isBusy = true;
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
        _isBusy = false;
        _pm.FreePlayer();
    }
    
    private void Update() {
        if (_isBusy && Input.GetKeyDown("space")) {
            AccelerateSentence();
        }
    }

    private EventTemplate EventFlagCheck(List<EventTemplate> events)
    {
        string waveNum = PlayerPrefs.GetInt("Wave", 0).ToString();
        
        foreach (var e in events)
        {
            if (e.EventFlag.Equals(waveNum))
            {
                return e;
            }
        }

        return events[0];
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private IEnumerator sentenceTyper = null;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, string name)
    {
        animator.SetBool("isOpen", true);
        sentences.Clear();
        // nameText.text = name;
        foreach (var sent in dialogue.sentences)
        {
            sentences.Enqueue(sent);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (sentenceTyper != null)
        {
            StopCoroutine(sentenceTyper);
        }

        sentenceTyper = TypeSentenceCourotine(sentence);
        StartCoroutine(sentenceTyper);
    }

    IEnumerator TypeSentenceCourotine(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        sentenceTyper = null;
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("ended convo");
    }
}

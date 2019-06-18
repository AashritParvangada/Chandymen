using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Queue<string> s_Queue_names = new Queue<string>();
    Queue<string> s_Queue_sentences = new Queue<string>();

    public Text Txt_NameText;
    public Text Txt_SentenceText;
    PlayerController plCont_playa;
    public Animator Anim_Animator;
    private void Start()
    {
        plCont_playa = FindObjectOfType<PlayerController>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        Anim_Animator.SetTrigger("Open");
        EnableOrDisableCont(false);
        ClearQueues();
        QueueSentences(_dialogue);
        CheckNextSentence();
    }

    void EnableOrDisableCont(bool _enabled)
    {
        plCont_playa.enabled = _enabled;
    }

    void ClearQueues()
    {
        s_Queue_names.Clear();
        s_Queue_sentences.Clear();
    }

    void QueueSentences(Dialogue _dialogue)
    {
        foreach (string name in _dialogue.S_Names)
        {
            s_Queue_names.Enqueue(name);
        }

        foreach (string sentence in _dialogue.S_Sentences)
        {
            s_Queue_sentences.Enqueue(sentence);

        }
    }
    public void CheckNextSentence()
    {
        if (s_Queue_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        DequeueAndDisplay();

    }
    void DequeueAndDisplay()
    {
        string name = s_Queue_names.Dequeue();
        Txt_NameText.text = name;

        string sentence = s_Queue_sentences.Dequeue();
        Txt_SentenceText.text = sentence;
    }
    void EndDialogue()
    {
        EnableOrDisableCont(true);
        Anim_Animator.SetTrigger("Close");

        Debug.Log("End of conversation");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E))
        {
            CheckNextSentence();
        }
    }
}

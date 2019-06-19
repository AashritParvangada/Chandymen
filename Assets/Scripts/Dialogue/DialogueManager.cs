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
    public Animator Anim_DialogBox;
    public GameObject[] GO_Arr_Characters;
    private void Start()
    {
        plCont_playa = FindObjectOfType<PlayerController>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
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

        DialogBoxAnimations(name);

        string sentence = s_Queue_sentences.Dequeue();
        Txt_SentenceText.text = sentence;
    }

    void DialogBoxAnimations(string _name)
    {
        if (_name == "jai")
        {
            Anim_DialogBox.SetTrigger("Open_Left");
        }

        else
        {
            Anim_DialogBox.SetTrigger("Open_Right");
        }

        foreach (GameObject _GO in GO_Arr_Characters)
        {
            _GO.SetActive(_GO.name == _name ? true : false);
        }
    }


    void EndDialogue()
    {
        EnableOrDisableCont(true);
        Anim_DialogBox.SetTrigger("Close");
        foreach (GameObject _GO in GO_Arr_Characters)
        {
            _GO.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E))
        {
            CheckNextSentence();
        }
    }
}

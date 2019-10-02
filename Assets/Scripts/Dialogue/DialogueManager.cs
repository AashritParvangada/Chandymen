using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Queue<string> s_Queue_names = new Queue<string>();
    Queue<string> s_Queue_sentences = new Queue<string>();
    DialogueTrigger dTrig_shutDownOnEnd;
    public Text Txt_NameText;
    public Text Txt_SentenceText;
    PlayerController plCont_playa;
    public Animator Anim_DialogBox, Anim_Name, Anim_Sentence;
    public GameObject[] GO_Arr_Characters;
    bool b_textAvailable = true, b_active = false;
    [SerializeField] AudioClip[] AudClp_CharacterVoices; AudioSource audioSource;
    private void Start()
    {
        GetVariables();
        Txt_NameText.enabled = false; Txt_SentenceText.enabled = false;
    }

    void GetVariables()
    {
        plCont_playa = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue _dialogue, DialogueTrigger _dtrig)
    {
        Debug.Log("Entered Start Dialogue");
        dTrig_shutDownOnEnd = _dtrig;
        b_active = true;
        Txt_NameText.enabled = true; Txt_SentenceText.enabled = true;
        EnableOrDisableCont(false);
        ClearQueues();
        QueueSentences(_dialogue);
        CheckNextSentence();
    }

    void EnableOrDisableCont(bool _enabled)
    {
        plCont_playa.GetComponent<Rigidbody>().velocity = Vector3.zero;
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


        audioSource.clip = AudClp_SelectSound(name);
        audioSource.Play();
    }

    void DialogBoxAnimations(string _name)
    {
        if (_name == "Jai")
        {
            Anim_DialogBox.SetTrigger("Open_Left");

            Anim_Name.SetTrigger("Place_Left");
            Anim_Name.GetComponentInParent<Text>().alignment = TextAnchor.UpperLeft;

            Anim_Sentence.SetTrigger("Place_Left");
            Anim_Sentence.GetComponentInParent<Text>().alignment = TextAnchor.UpperLeft;
        }

        else
        {
            Anim_DialogBox.SetTrigger("Open_Right");

            Anim_Name.SetTrigger("Place_Right");
            Anim_Name.GetComponentInParent<Text>().alignment = TextAnchor.UpperRight;

            Anim_Sentence.SetTrigger("Place_Right");
            Anim_Sentence.GetComponentInParent<Text>().alignment = TextAnchor.UpperRight;
        }

        foreach (GameObject _GO in GO_Arr_Characters)
        {
            _GO.SetActive(_GO.name == _name ? true : false);
        }

        //Sounds


    }

    AudioClip AudClp_SelectSound(string _name)
    {
        List<AudioClip> _audClpsList = new List<AudioClip>();
        foreach (AudioClip _audClp in AudClp_CharacterVoices)
        {
            if (_audClp.name.Contains(_name))
            {
                _audClpsList.Add(_audClp);
            }
        }

        return _audClpsList[Random.Range(0, _audClpsList.Count-1)];

    }

    void EndDialogue()
    {
        EnableOrDisableCont(true);
        Anim_DialogBox.SetTrigger("Close");
        foreach (GameObject _GO in GO_Arr_Characters)
        {
            _GO.SetActive(false);
        }
        dTrig_shutDownOnEnd.gameObject.SetActive(false);
        Txt_NameText.enabled = false; Txt_SentenceText.enabled = false;
        FindObjectOfType<EventManager>().FinishDialogueEvent();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E)) && b_active)
        {
            CheckNextSentence();
        }
    }
}

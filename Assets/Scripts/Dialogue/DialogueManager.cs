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
    public Animator Anim_DialogBox, Anim_Name, Anim_Sentence, Anim_DBButton;
    public GameObject[] GO_Arr_Characters;
    GameObject go_playerHealth;
    bool b_textAvailable = true, b_active = false, b_coolingDown = false;
    public bool B_IsEndCredits = false;
    [SerializeField] AudioClip[] AudClp_CharacterVoices; AudioSource audioSource;
    [SerializeField] GameObject Go_TimboiDeathDialogue;
    Controls ctrl_Controls;
    private void OnEnable()
    {
        EventManager.OnTimboiHealthDepleted += ActivateTimboiDeathDialogue;
    }

    private void OnDisable()
    {
        EventManager.OnTimboiHealthDepleted -= ActivateTimboiDeathDialogue;
    }

    private void Start()
    {
        GetVariables();
        Txt_NameText.enabled = false; Txt_SentenceText.enabled = false;
    }

    void GetVariables()
    {
        plCont_playa = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        go_playerHealth = FindObjectOfType<HealthBar>().gameObject;
        ctrl_Controls = FindObjectOfType<Controls>();
    }

    public void StartDialogue(Dialogue _dialogue, DialogueTrigger _dtrig)
    {
        dTrig_shutDownOnEnd = _dtrig;
        b_active = true;
        Txt_NameText.enabled = true; Txt_SentenceText.enabled = true;
        ctrl_Controls.gameObject.SetActive(false);
        go_playerHealth.SetActive(false);
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
            Anim_DBButton.SetTrigger("Right");

            Anim_Name.SetTrigger("Place_Left");
            Anim_Name.GetComponentInParent<Text>().alignment = TextAnchor.UpperLeft;

            Anim_Sentence.SetTrigger("Place_Left");
            Anim_Sentence.GetComponentInParent<Text>().alignment = TextAnchor.UpperLeft;
        }

        else
        {
            Anim_DialogBox.SetTrigger("Open_Right");
            Anim_DBButton.SetTrigger("Left");

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

        return _audClpsList[Random.Range(0, _audClpsList.Count)];

    }

    void EndDialogue()
    {
        EnableOrDisableCont(true);
        Anim_DialogBox.SetTrigger("Close");
        Anim_DBButton.SetTrigger("Close");

        foreach (GameObject _GO in GO_Arr_Characters)
        {
            _GO.SetActive(false);
        }
        dTrig_shutDownOnEnd.gameObject.SetActive(false);
        Txt_NameText.enabled = false; Txt_SentenceText.enabled = false;
        go_playerHealth.SetActive(true);
        FindObjectOfType<EventManager>().FinishDialogueEvent();
        ctrl_Controls.gameObject.SetActive(true);

        if (B_IsEndCredits) FindObjectOfType<EventManager>().EndCreditTriggerEvent();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.E)) && b_active && !b_coolingDown)
        {
            CheckNextSentence();
            StartCoroutine(IEnum_Cooldown());
        }
    }

    void ActivateTimboiDeathDialogue()
    {
        Go_TimboiDeathDialogue.SetActive(true);
    }

    IEnumerator IEnum_Cooldown()
    {
        b_coolingDown = true;
        yield return new WaitForSeconds(.5f);
        b_coolingDown = false;
    }
}

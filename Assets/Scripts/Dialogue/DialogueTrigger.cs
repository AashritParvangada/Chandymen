using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dia_Dialogue;
    [SerializeField] bool B_isEndCreditDialogue = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dia_Dialogue, this);

        if (B_isEndCreditDialogue)
        {
            FindObjectOfType<DialogueManager>().B_IsEndCredits = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            TriggerDialogue();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dia_Dialogue;
    [SerializeField] bool isEndCreditDialogue=false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dia_Dialogue, this);

        if(isEndCreditDialogue)
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            TriggerDialogue();
    }

}

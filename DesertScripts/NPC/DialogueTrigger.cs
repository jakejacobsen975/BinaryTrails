using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;
    public DialogueManager dialogueManager; // Add this line

    public GameObject Object;

    public void TriggerDialogue() {

        Object.SetActive(true);
        dialogueManager.StartDialogue(dialogue);
    }
}

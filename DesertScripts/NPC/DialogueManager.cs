using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{

    public Text nameText;
    public Text dialogueText;

    public GameObject panel;

    public GameObject namePanel;
    public GiveGun giveItem;

    public TopDownController player;

    public BulletDisplay bulletDisplay;


    Queue<string> sentences;
    void Start(){
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue){
        if(dialogue.name != null){
            nameText.text = dialogue.name;
        }

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNexSentence();
    }
    public void DisplayNexSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        if (sentences.Count == 0){
            if(player.hasGun){
                EndDialogue();
                
            }else{
                giveItem.OpenClose(true);
                namePanel.SetActive(false);
            }
            
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    void EndDialogue(){
        panel.SetActive(false);
        Time.timeScale = 1f;
        if(giveItem != null){
            giveItem.giveGun();
            giveItem.OpenClose(false);
        }
    }
    IEnumerator TypeSentence( string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

   
}

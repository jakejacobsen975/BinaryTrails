using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> correct;
    public List<Sprite> incorrect;
    public List<Sprite> regular;
    public string letter;

    BinaryPlayerMovement binaryMovement;

    int letterIndex;
    multipleChoice parentObject;

    void Start(){
        parentObject = GetComponentInParent<multipleChoice>();
        binaryMovement = FindAnyObjectByType<BinaryPlayerMovement>(); 
        
        if(letter == "A"){
            letterIndex = 0;
        }else if(letter == "B"){
            letterIndex = 1;
        }else if(letter == "C"){
            letterIndex = 2;
        }else{
            letterIndex = 3;
        }
    }
    void Update(){
        if(parentObject.correctAnswerChosen){
            spriteRenderer.sprite = correct[letterIndex];
        }
    }

    private IEnumerator OnCollisionEnter2DCoroutine(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            if (letter == parentObject.correctAnswer){
                spriteRenderer.sprite = correct[letterIndex];
                parentObject.correctAnswerChosen = true;
            }else{
                spriteRenderer.sprite = incorrect[letterIndex];
                binaryMovement.playerHealth.TakeDamage(20);
                yield return new WaitForSeconds(1f);
                spriteRenderer.sprite = regular[letterIndex];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        StartCoroutine(OnCollisionEnter2DCoroutine(collision));
    }
}

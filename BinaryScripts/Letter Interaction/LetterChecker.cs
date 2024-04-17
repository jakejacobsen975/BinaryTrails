using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterChecker : MonoBehaviour
{
    
    public CheckAnswer checkAnswer;

    public SpriteRenderer spriteRenderer;
    public Sprite correctSprite;

    public Sprite incorrectSprite;

    public bool IsCorrectBlock;
    

    void Update(){
      if(checkAnswer != null){
      if(checkAnswer.IsBinaryCorrect()){
        IsCorrectBlock = true;
        spriteRenderer.sprite = correctSprite;
      }else{
        spriteRenderer.sprite = incorrectSprite;
      }
        }
    }
}

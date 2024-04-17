using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberInteraction : MonoBehaviour{
    public List<Sprite> blackSprites;

    public List<Sprite> redSprites;
    public List<Sprite> greenSprites;
    
    public int currentBlock;

    public blockState currentState = blockState.BLACK;

    public enum blockState{
        BLACK,
        RED,
        GREEN
    }
    public SpriteRenderer spriteRenderer;

    public bool isChangeable = true;
    void Update(){
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") ){
            if(currentState == blockState.BLACK && isChangeable){
                currentState = blockState.GREEN;
                spriteRenderer.sprite = greenSprites[currentBlock];
            }else if(currentState == blockState.GREEN && isChangeable){
                currentState = blockState.BLACK;
                spriteRenderer.sprite = blackSprites[currentBlock];
            }
        }
    }
    public void SetCorrect(){
        currentState = blockState.GREEN;
        spriteRenderer.sprite = greenSprites[currentBlock];
        isChangeable = false;
    }
    public void SetIncorrect(){
        currentState = blockState.RED;
        spriteRenderer.sprite = redSprites[currentBlock];
    }
    public void SetBlack(){
        currentState = blockState.BLACK;
        spriteRenderer.sprite = blackSprites[currentBlock];
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    
    public SpriteRenderer spriteRenderer;

    public Sprite one;

    public Sprite zero;

    public int currentBlock;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") ){
            if(currentBlock == 1){
                spriteRenderer.sprite = zero;
                currentBlock = 0;
            }else{
                 spriteRenderer.sprite = one;
                 currentBlock = 1;
            }
        }
    }
}

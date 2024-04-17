using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePortal : MonoBehaviour{
    public float frameRate;

    public List<Sprite> Sprites;


    public Sprite emptyPortal;

    public SpriteRenderer spriteRenderer;




    void Update(){
        
        int totalFrames = (int)(Time.time * frameRate);
        int frame = totalFrames % Sprites.Count;
        spriteRenderer.sprite = Sprites[frame];
           
    }
}

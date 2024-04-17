using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class SpriteSheets : MonoBehaviour
{
    private int currentDirection = 4;
    public List<Sprite> nMaleSprites;
    public List<Sprite> neMaleSprites;
    public List<Sprite> eMaleSprites;
    public List<Sprite> seMaleSprites;
    public List<Sprite> sMaleSprites;

    public List<Sprite> nMaleShootingSprites;
    public List<Sprite> neMaleShootingSprites;
    public List<Sprite> eMaleShootingSprites;
    public List<Sprite> seMaleShootingSprites;
    public List<Sprite> sMaleShootingSprites;

    public List<Sprite> MaleIdleSprites;

    public List<Sprite> MaleDeath;
    public List<Sprite> nFemaleSprites;
    public List<Sprite> neFemaleSprites;
    public List<Sprite> eFemaleSprites;
    public List<Sprite> seFemaleSprites;
    public List<Sprite> sFemaleSprites;

    public List<Sprite> nFemaleShootingSprites;
    public List<Sprite> neFemaleShootingSprites;
    public List<Sprite> eFemaleShootingSprites;
    public List<Sprite> seFemaleShootingSprites;
    public List<Sprite> sFemaleShootingSprites;

    public List<Sprite> FemaleIdleSprites;

    public List<Sprite> FemaleDeath;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Sprite> GetSpriteDirection( Vector2 direction,Gender gender){
        List<Sprite> selectedSprites = null;

            if (direction.y > 0){// north
                if (Math.Abs(direction.x) > 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = neFemaleSprites;
                    }else{
                        selectedSprites = neMaleSprites;
                    }
                    currentDirection = 1;
                }
                else{
                    if(gender == Gender.Female){
                        selectedSprites = nFemaleSprites;
                    }else{
                        selectedSprites = nMaleSprites;
                    }
                    currentDirection = 0;
                }
            }
            else if (direction.y < 0){// south
                if (Math.Abs(direction.x) > 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = seFemaleSprites;
                    }else{
                        selectedSprites = seMaleSprites;
                    }
                    currentDirection = 3;
                }
                else{
                    if(gender == Gender.Female){
                        selectedSprites = sFemaleSprites;
                    }else{
                        selectedSprites = sMaleSprites;
                    }
                    currentDirection = 4;
                }
            }
            else{
                if (Math.Abs(direction.x) > 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = eFemaleSprites;
                    }else{
                        selectedSprites = eMaleSprites;
                    }
                    currentDirection = 2;
                }
            }
    
        return selectedSprites;
    }
public List<Sprite> GetSpriteShooting(Vector3 aimInput,Gender gender){
        List<Sprite> selectedSprites = null;
            

            if (Mathf.RoundToInt(aimInput.y) > 0){// north
                if (Math.Abs(Mathf.RoundToInt(aimInput.x)) != 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = neFemaleShootingSprites;
                    }else{
                        selectedSprites = neMaleShootingSprites;
                    }
                }
                else{
                    if(gender == Gender.Female){
                        selectedSprites = nFemaleShootingSprites;
                    }else{
                        selectedSprites = nMaleShootingSprites;
                    }
                }
            }
            else if (Mathf.RoundToInt(aimInput.y) < 0){// south
                if (Math.Abs(Mathf.RoundToInt(aimInput.x)) > 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = seFemaleShootingSprites;
                    }else{
                        selectedSprites = seMaleShootingSprites;
                    }
                }
                else{
                    if(gender == Gender.Female){
                        selectedSprites = sFemaleShootingSprites;
                    }else{
                        selectedSprites = sMaleShootingSprites;
                    }
                }
            }
            else{
                if (Math.Abs(aimInput.x) > 0){// east or west
                    if(gender == Gender.Female){
                        selectedSprites = eFemaleShootingSprites;
                    }else{
                        selectedSprites = eMaleShootingSprites;
                    }
                }
            }
    
        return selectedSprites;
    }
    public Sprite getIdleSprite(Gender gender){
        Sprite sprite;
        if(gender == Gender.Female){
            sprite = FemaleIdleSprites[currentDirection];
        }else{
            sprite = MaleIdleSprites[currentDirection];
            
        }   
         
        return sprite;
    }
    public List<Sprite> getDeathSprites(Gender gender){
        List<Sprite> selectedSprites = null;
        if(gender == Gender.Female){
            selectedSprites = FemaleDeath;
        }else{
            selectedSprites = MaleDeath;
        }
        return selectedSprites;
    }
    
}

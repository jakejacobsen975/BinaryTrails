using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class BinaryPlayerMovement : MonoBehaviour{
    // movement
    private float move;

    public float jump;

    public bool isJumping;
    public float frameRate;
    public Rigidbody2D body;
    public float walkSpeed;

    // sprites 
    public SpriteRenderer spriteRenderer;
    public List<Sprite> walkingSprites;
    public List<Sprite> jumpingSprite;
    public List<Sprite> idleSprite;
    public List<Sprite> femaleWalkingSprites;
    public List<Sprite> femaleJumpingSprite;
    public List<Sprite> femaleIdleSprite;

    public PlayerHealth playerHealth;
    public int damageAmount = 5;
    float lastDamageTime = -Mathf.Infinity;

    float damageCooldown = 1f;

    public string currentLevel;

    Gender gender = Gender.Female;


    void Start(){
        PlayerData data = SaveData.loadPlayer();
        gender = data.gender;
    }
    // the commented out segments are from https://www.youtube.com/watch?v=BLfNP4Sc_iA&list=WL&index=26&t=205s
    void Update(){
        // move = Input.GetAxis("Horizontal");
       
        // body.velocity =  new Vector2(walkSpeed * move, body.velocity.y);

        // if(Input.GetButtonDown("Jump") && !isJumping && Time.timeScale == 1f){
        //     body.AddForce(new Vector2(body.velocity.x,jump));
        // }
            
        
        HandleAnimations();
        if (playerHealth.currentHealth <= 0 || transform.position.y < -30){
            body.velocity = Vector2.zero;
            StartCoroutine(PlayDeathAnimation());
        }
    }
    IEnumerator PlayDeathAnimation(){
        if (!spriteRenderer.flipY){
            spriteRenderer.flipY = true;
        }
        
    for (int frame = 0; frame < 5; frame++){
        if(gender == Gender.Female){
            spriteRenderer.sprite = femaleIdleSprite[0];
        }else{
            spriteRenderer.sprite = idleSprite[0];
        }
        yield return new WaitForSeconds(1f / frameRate);
    }
    SceneManager.LoadScene(currentLevel);

    
    
}
    private void HandleAnimations(){
        // handle direction
        if (!spriteRenderer.flipX && move < 0 && Time.timeScale == 1f){
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && move > 0 && Time.timeScale == 1f){
            spriteRenderer.flipX = false;
        }

        List<Sprite> directionSprites = GetSpriteDirection();
        
        if(Time.timeScale == 1f){
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        
        
    }
     List<Sprite> GetSpriteDirection(){

        List<Sprite> selectedSprites;

            if (isJumping){
                if(gender == Gender.Female){
                    selectedSprites = femaleJumpingSprite;
                }else{
                    selectedSprites = jumpingSprite;
                }
            }else{
                if (Math.Abs(move) > 0){// left or right direction then walk
                    if(gender == Gender.Female){
                        selectedSprites = femaleWalkingSprites;
                    }else{
                        selectedSprites = walkingSprites;
                    }
                }else{
                    if(gender == Gender.Female){
                        selectedSprites = femaleIdleSprite;
                    }else{
                        selectedSprites = idleSprite;
                    }
                }
            }
            
          
        
            
        return selectedSprites;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // if(collision.gameObject.CompareTag("Ground")){
        //     isJumping = false;
        // }
        if(collision.gameObject.CompareTag("Enemy")){
            Enemy_AI_binary enemyScript = collision.gameObject.GetComponent<Enemy_AI_binary>();
            CheckAndDealDamage(enemyScript);
        }
    }
    void OnCollisionStay2D(Collision2D collision){
    if (collision.gameObject.CompareTag("Enemy")){
        Enemy_AI_binary enemyScript = collision.gameObject.GetComponent<Enemy_AI_binary>();
        CheckAndDealDamage(enemyScript);
        }
        if(collision.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            isJumping = true;
        }
    }
    void CheckAndDealDamage(Enemy_AI_binary enemyScript){
        if (Time.time - lastDamageTime >= damageCooldown){
            // Calculate the vertical distance between player and enemy
            float verticalDistance = transform.position.y - enemyScript.transform.position.y;

            // Check if the player is above the enemy and not too high above it
            if (verticalDistance > .75f){
                if (enemyScript != null){
                    Vector2 knockBackForce = new Vector2(0f, 6f); // You can adjust the force as needed
                    body.AddForce(knockBackForce, ForceMode2D.Impulse);
                    enemyScript.enemyHealth.TakeDamage(damageAmount);
                }
                lastDamageTime = Time.time;
            }
        }
    }
}
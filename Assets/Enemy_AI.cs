using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI : MonoBehaviour{    
    // Death Animation
     public List<Sprite> Death;


     public EnemyHealth enemyHealth;

    // Move Animation
    public List<Sprite> MoveRight;
    public List<Sprite> MoveLeft;
    public List<Sprite> MoveUp;
    public List<Sprite> MoveDown;
    public List<Sprite> Idle;
    // Attack Animation 
    public List<Sprite> AttackRight;
    public List<Sprite> AttackLeft;
    public List<Sprite> AttackUp;
    public List<Sprite> AttackDown;

    public SpriteRenderer spriteRenderer;

    public float frameRate = 4f;
    public PlayerHealth player;

    public int damageAmount = 5;
    float lastDamageTime = -Mathf.Infinity;

    Vector2 direction;


    public Transform target;

    public Transform EnemyGFX;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;

    public float spriteChangeDelay = 0.1f; 

    float lastSpriteChangeTime = 0f;

    Pathfinding.Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    enum State {
        idle,
        moving,
        attacking
    }
    State currentState = State.idle;

    bool isPlayingAnimation = false;

    Seeker seeker;
    Rigidbody2D rb;

    // the commented out code is from https://www.youtube.com/watch?v=jvtFUfJ6CP8&list=WL&index=26&t=185s
    // however some of the code is outdated and I had to figure out how to update it 

    void Start(){
        // seeker = GetComponent<Seeker>();
        // rb = GetComponent<Rigidbody2D>();

        // InvokeRepeating("UpdatePath",0f,0.25f);
    }
    // void OnPathComplete(Pathfinding.Path p){
    //     if(!p.error){
    //         path = p;
    //         currentWaypoint = 0;

    //     }
    // }
    // void UpdatePath(){
    //     if(seeker.IsDone()){
    //         seeker.StartPath(rb.position,target.position, OnPathComplete);
    //     }

    // }
    // Update is called once per frame
void Update()
{   
    if (enemyHealth != null){
        if (!isPlayingAnimation){
            checkPath();
            if (CheckForAttack()){
                Attack(direction);
            }
            switch (currentState)
            {
                case State.idle:
                    SetIdleSprite();
                    break;
                    
                case State.moving:
                    SetMovingSprite();
                    break;
            }
            
            if (enemyHealth.currentHealth <= 0){
            
                StartCoroutine(PlayDeathAnimation());
            }
        }
    }
    
    
    
}   
   IEnumerator PlayDeathAnimation(){
        rb.velocity = Vector2.zero;
        
        isPlayingAnimation = true;

        if (enemyHealth != null && enemyHealth.healthBar != null){
            enemyHealth.healthBar.gameObject.SetActive(false);
        }
        for (int frame = 0; frame < Death.Count; frame++){
            spriteRenderer.sprite = Death[frame];
            yield return new WaitForSeconds(.5f);
        }

    
        gameObject.SetActive(false);
        
        isPlayingAnimation = false;
    }
    IEnumerator PlayAttackAnimation(List<Sprite> attackSprites){
        isPlayingAnimation = true;
        foreach (Sprite sprite in attackSprites){
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(.3f); 
        }
        currentState = State.idle;
        isPlayingAnimation = false;

    }
    void checkPath(){
        // if (path == null)
        //     return;

        // if (currentWaypoint >= path.vectorPath.Count)
        // {
        //     reachedEndOfPath = true;
        //     return;
        // }
        // else
        // {
        //     reachedEndOfPath = false;
        // }
        

        // direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Vector2 force = direction * speed * Time.deltaTime;

        // rb.AddForce(force);

        // float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // if (distance < nextWaypointDistance){
        //     currentWaypoint++;
        // }
            

        if(path.vectorPath.Count > 12){
            currentState = State.idle;
            path = null;
        }else{
            currentState = State.moving;
            
        }
        
                
        
        
    }
    void SetIdleSprite()
{
    // Set idle sprite
    if (Time.time - lastSpriteChangeTime > spriteChangeDelay)
    {
        int totalFrames = (int)(Time.time * frameRate);
        int frame = totalFrames % Idle.Count;
        spriteRenderer.sprite = Idle[frame];
        lastSpriteChangeTime = Time.time;
    }
}

void SetMovingSprite()
{
    List<Sprite> directionSprites = GetSpriteDirection(direction);
    if (directionSprites != null)
    {
        if (Time.time - lastSpriteChangeTime > spriteChangeDelay)
        {
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % directionSprites.Count;
            spriteRenderer.sprite = directionSprites[frame];
            lastSpriteChangeTime = Time.time;
        }
    }
    else
    {
        if (Time.time - lastSpriteChangeTime > spriteChangeDelay)
        {
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % Idle.Count; 
            spriteRenderer.sprite = Idle[frame];
            lastSpriteChangeTime = Time.time;
        }
    }
}
        

    List<Sprite> GetSpriteDirection(Vector2 direction){
        // Update the sprite direction
        List<Sprite> selectedSprites = null;

       
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)){
        // Horizontal movement
        if (direction.x < 0.1f){
            selectedSprites = MoveLeft;

        } else if (direction.x > -0.1f){
            selectedSprites = MoveRight;

            
        }
        } else {
            // Vertical movement
            if (direction.y > 0.1f){
                selectedSprites = MoveUp;

            } else if (direction.y < -0.1f){
                selectedSprites = MoveDown;
            }else{
                selectedSprites = Idle;
            }
        }

        if (selectedSprites == null){
            selectedSprites = Idle;
        }
        

        return selectedSprites;
    }
    bool CheckForAttack(){
       bool isAttack = false;
       float attackDistanceThreshold = 2f; 

        float distanceToTarget = Vector2.Distance(target.position, EnemyGFX.position);

        if (distanceToTarget < attackDistanceThreshold){
            isAttack = true;
            
        }
        return isAttack;
    }
    void Attack(Vector2 direction){
    List<Sprite> attackSprites = null;

    if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)){
        // Horizontal movement
        if (direction.x < 0.01f){
            attackSprites = AttackLeft;
        } else if (direction.x > -0.01f){
            attackSprites = AttackRight;
        }
    } else {
        // Vertical movement
        if (direction.y > 0.01f){
            attackSprites = AttackUp;
        } else if (direction.y < -0.01f){
            attackSprites = AttackDown;
        }
    }

    if (attackSprites != null ){
        StartCoroutine(PlayAttackAnimation(attackSprites));
          
        }
    float damageCooldown = 1.0f;
    while (Time.time - lastDamageTime >= damageCooldown){
        player.TakeDamage(damageAmount);
        lastDamageTime = Time.time; 
    }
}

   
}

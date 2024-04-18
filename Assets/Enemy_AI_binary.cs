using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI_binary : MonoBehaviour
{
    public List<Sprite> Death;

     public EnemyHealth enemyHealth;
    // Move Animation
    public List<Sprite> MoveRight;
    public List<Sprite> MoveLeft;
    public List<Sprite> Idle;
 
    
    

    public SpriteRenderer spriteRenderer;

    public float frameRate = 4f;
    public PlayerHealth player;


    float damageCooldown = 1f;
    int damageAmount = 5;
    float lastDamageTime = -Mathf.Infinity;

    Vector2 direction;


    public Transform target;

    public Transform EnemyGFX;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Pathfinding.Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    
    public float jumpForce = 20f;

    private float lastJumpTime = -Mathf.Infinity;

    private bool isGrounded = true;

    void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath",0f,0.25f);
    }
    void OnPathComplete(Pathfinding.Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;

        }
    }
    void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(rb.position,target.position, OnPathComplete);
        }

    }
    // Update is called once per frame
void Update()
{   
    
    if (enemyHealth != null){
        SetSprites();
        
        if (enemyHealth.currentHealth <= 0 || transform.position.y < -30){
             rb.velocity = Vector2.zero;
            StartCoroutine(PlayDeathAnimation());
        }
    }
    if (isGrounded && target.position.y > rb.position.y){
        TryJump();
    }
    
}   
    void TryJump(){
        // Debug.Log("Last Jump Time: " + lastJumpTime);
        if (Time.time - lastJumpTime >= 1f){
            rb.AddForce(new Vector2(rb.velocity.x,jumpForce));
            lastJumpTime = Time.time; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
        if(collision.gameObject.CompareTag("Player")){
            
            if(Time.time - lastDamageTime >= damageCooldown){
                TryDealDamage();
            }
    }
    }
    void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Player")){
            TryDealDamage();
            }
        }

    void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
    void TryDealDamage(){
    if (Time.time - lastDamageTime >= damageCooldown){
        // Calculate the vertical distance between enemy and player
        float verticalDistance = player.transform.position.y - transform.position.y;

        // Check if the player is not directly above the enemy (head)
        if (verticalDistance < .75f){
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
            lastDamageTime = Time.time;
        }
    }
}
   IEnumerator PlayDeathAnimation(){
        if (enemyHealth != null && enemyHealth.healthBar != null){
            enemyHealth.healthBar.gameObject.SetActive(false);
        }
        if (!spriteRenderer.flipY){
            spriteRenderer.flipY = true;
        }
        
        for (int frame = 0; frame < 5; frame++){
            spriteRenderer.sprite = Death[0];
            yield return new WaitForSeconds(1f / frameRate);
        }
    gameObject.SetActive(false);
    
}
    void SetSprites(){
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
            List<Sprite> directionSprites;

        if(path.vectorPath.Count > 10){
            directionSprites = Idle;
            path = null;
        }else{
            directionSprites = GetSpriteDirection(direction);
        }
        
                
        if (directionSprites != null)
        {
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % directionSprites.Count;
            spriteRenderer.sprite = directionSprites[frame];

            
        }
        else
        {
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % Idle.Count;
            spriteRenderer.sprite = Idle[frame];
        }
    }
        

    List<Sprite> GetSpriteDirection(Vector2 direction){
        // Update the sprite direction
        List<Sprite> selectedSprites = null;
        
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y)){
        // Horizontal movement
        if (direction.x < 0.01f){
            selectedSprites = MoveLeft;

        } else if (direction.x > -0.01f){
            selectedSprites = MoveRight;
            
        }
        }else{
            selectedSprites = Idle;
        }
        

        if (selectedSprites == null){
            selectedSprites = Idle;
        }
        

        return selectedSprites;
    }

   
}

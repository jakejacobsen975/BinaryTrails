using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;




public class TopDownController : MonoBehaviour {
    
    // player interactions 
    
    public Gender gender;
    public Rigidbody2D body;

    ShootingBehavior shootingBehavior;

    public PlayerHealth playerHealth;
    // sprites 
    public SpriteRenderer spriteRenderer;

    
    public SpriteSheets spriteSheet;
    // player animations 
    public float walkSpeed;
    public float frameRate;

    public bool hasGun;

    public int numberOfBullets;

    private Vector2 direction;
    public Vector2 Direction{
    get { return direction; }
    set { direction = value.normalized; }
    }
    // collisions 
    public LayerMask objectsLayer;

    // idle sprite index

    // player state
    public List<string> completedLevels = new List<string>();


    public WinningScript winningScript;
    

   void Start() {
        shootingBehavior = GetComponent<ShootingBehavior>();
        if (shootingBehavior == null) {
            Debug.LogError("ShootingBehavior component not found on the same GameObject.");
        }
        LoadGame();
        int newNumberBullets = 0;

        

        foreach(string completedLevel in completedLevels){
            if(completedLevel == "Tutorial"){
                newNumberBullets = 1;
            }
        }
        
        for ( int i = 0; i < completedLevels.Count-1; i++){
            newNumberBullets *= 2; 
        }
    
        numberOfBullets = newNumberBullets;
        
        
        if(completedLevels.Count >= 5){
            winningScript.winningMenuActive();
        }
    }
    

    void Update(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        if (isWalkAble()){
        body.velocity = direction * walkSpeed;
        } else {
            body.velocity = Vector2.zero;
        }
        if(Input.GetButton("Cancel")){
            Debug.Log("player options");
        }
        if (playerHealth != null){
            HandleWalkingNIdle();
            if(hasGun && numberOfBullets > 0){
                shootingBehavior.Crosshair.SetActive(true);
                shootingBehavior.AimNShoot();
                shootingBehavior.HandleShootingCoolDown();
            }else{
                shootingBehavior.Crosshair.SetActive(false);
            }
            if (playerHealth.currentHealth <= 0){
                body.velocity = Vector2.zero;
                StartCoroutine(PlayDeathAnimation());
            }
    }
    
    
    
}   

   IEnumerator PlayDeathAnimation(){

    List<Sprite> Death = spriteSheet.getDeathSprites(gender);
    
    float animationDuration = Death.Count / frameRate;
    float timer = 0.0f;

    while (timer < animationDuration)
    {
        int frame = Mathf.FloorToInt(timer * frameRate) % Death.Count;
        spriteRenderer.sprite = Death[frame];
        timer += Time.deltaTime;
        yield return null;
        
    }
    SaveData.resetPosition();
    SceneManager.LoadScene("Desert");
    
    }

    



    private void HandleWalkingNIdle(){
        // handle direction
        if (!spriteRenderer.flipX && direction.x < 0){
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0){
            spriteRenderer.flipX = false;
        }

        List<Sprite> directionSprites = spriteSheet.GetSpriteDirection(direction,gender);
        
        if (directionSprites != null ){
            int totalFrames = (int)(Time.time * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }else{
            spriteRenderer.sprite = spriteSheet.getIdleSprite(gender);
        }
        
    }
    bool isWalkAble(){
    // Calculate the next position based on the current position and direction
    Vector2 nextPosition = (Vector2)transform.position + Direction * 0.2f;

    // Check if there is an overlap at the next position with objects on the specified layer
    if (Physics2D.OverlapCircle(nextPosition, 0.2f, objectsLayer) != null){
        return false;
    }
    return true;
    }
    public void SaveGame() {
        SaveData.SavePlayerData(this);
    }
    public void LoadGame( ) {
        PlayerData data = SaveData.loadPlayer();

        if (data != null){
            completedLevels = data.completedLevels;
            gender = data.gender;
            hasGun = data.hasGun;
            Vector2 position;
            position.x = data.position[0];
            position.y = data.position[1];
            transform.position = position;
        }else{
            transform.position = new Vector2(0,0);
            completedLevels = new List<string>();
        }

    }
    public void setGun(bool gun){
        hasGun = gun;
    }
}

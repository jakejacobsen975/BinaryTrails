using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour{
   
    public GameObject bulletPrefab;
    public GameObject Crosshair;

    public TopDownController player;

    public SpriteSheets spriteSheet;

    public bool isShooting = false;
    public float shootingTimer = 0.0f;
    private float shootingDuration = 0.5f;
    private bool isRTButtonPressed = false;
    
    private List<GameObject> bullets = new List<GameObject>();

    private bool isShootingCoolDown = false;
    private float shootingCoolDownDuration = 1.0f; 
    private float shootingCoolDownTimer = 0.0f;
    public void AimNShoot(){
        Crosshair.SetActive(false);
        float shootingSpeed = 20.0f;


    // some of this code was inspired by https://www.youtube.com/watch?v=_QVAC69su3Q&list=WL&index=20&t=266s
    // however because I have changed it so drastically I don't feel commenting out the things that I worked hard on changing should be commented out.
        if (Input.GetButton("Fire2") || Input.GetAxis("LT") > 0)
        {
            Crosshair.SetActive(true);
            
            // Vector3 aimInput = Vector3.zero;
            // Vector2 shootingDirection;

            if (Input.GetButton("Fire2"))
            {
            Vector3 aimMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                aimMouse.z = 0.0f;
                aimInput = aimMouse - transform.position;
                aimInput = RestrictTo8Directions(aimInput);
                aimInput.Normalize();
                
                if (Input.GetButtonDown("Fire1") && !isShootingCoolDown)
                {
                    shootingDirection = (aimMouse - transform.position).normalized;
                    HandleShooting(aimInput,shootingDirection, shootingSpeed);
                    isShootingCoolDown = true;
                }
            }
            else if (Input.GetAxis("LT") > 0)
            {
                Vector3 aimController = new Vector3(Input.GetAxis("AimHorizontalCon"), Input.GetAxis("AimVerticalCon"), 0.0f);
                aimController.Normalize();
                aimInput = aimController;
                if (Input.GetAxis("RT") > 0 && !isRTButtonPressed && (Input.GetAxis("AimHorizontalCon") != 0 || Input.GetAxis("AimVerticalCon") != 0)&& !isShootingCoolDown)
                {
                    shootingDirection = new Vector2(Input.GetAxis("AimHorizontalCon"), Input.GetAxis("AimVerticalCon"));
                    shootingDirection.Normalize();
                    HandleShooting(aimInput,shootingDirection, shootingSpeed);
                    isRTButtonPressed = true;
                    isShootingCoolDown = true;
                }
                if (Input.GetAxis("RT") == 0)
                {
                    isRTButtonPressed = false;
                }
            }
            
            
            Crosshair.transform.localPosition = aimInput * 3.0f;
            
        }
}
private void HandleShooting(Vector3 aimInput, Vector2 shootingDirection, float shootingSpeed)
{

    for (int i = 0; i < player.numberOfBullets; i++)
    {
        Vector2 adjustedDirection = shootingDirection;
        
        if (player.numberOfBullets > 1)
        {
            // adjustedDirection = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-15f, 15f)) * shootingDirection; chatGPT
        }

        // GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); chatGPT
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.velocity = adjustedDirection * shootingSpeed;
        bulletScript.player = gameObject;
        bullets.Add(bullet);
    }

    bool shouldFlipX = Mathf.RoundToInt(shootingDirection.x) < 0;

    List<Sprite> directionSprites = spriteSheet.GetSpriteShooting(aimInput,player.gender);

    if (!isShooting)
    {
        isShooting = true;
        StartCoroutine(PlayShootingAnimation(directionSprites, shouldFlipX));
    }
}

private IEnumerator PlayShootingAnimation(List<Sprite> sprites,  bool shouldFlipX)
{
    float animationDuration = sprites.Count / player.frameRate;
    float timer = 0.0f;

    while (timer < animationDuration)
    {
        int frame = Mathf.FloorToInt(timer * player.frameRate) % sprites.Count;
        player.spriteRenderer.flipX = shouldFlipX;
        player.spriteRenderer.sprite = sprites[frame];
        timer += Time.deltaTime;
        yield return null;
    }

    isShooting = false;
    foreach (var bullet in bullets)
    {
        Destroy(bullet,1.0f);
    }
    bullets.Clear();
    
}
// private Vector3 RestrictTo8Directions(Vector3 input)
// {
//     float angle = Mathf.Atan2(input.y, input.x);
//     int octant = Mathf.RoundToInt(8 * angle / (2 * Mathf.PI) + 8) % 8;
//     float restrictedAngle = octant * (2 * Mathf.PI) / 8;

//     float restrictedX = Mathf.Cos(restrictedAngle);
//     float restrictedY = Mathf.Sin(restrictedAngle);

//     // Ensure values are clamped between -1 and 1
//     restrictedX = Mathf.Clamp(restrictedX, -1f, 1f);
//     restrictedY = Mathf.Clamp(restrictedY, -1f, 1f);

//     return new Vector3(restrictedX, restrictedY, 0.0f);
// } there was some complex math involved so I got help from chatGPT
public void HandleShootingCoolDown(){
    if (isShooting)
        {
            shootingTimer += Time.deltaTime;
            player.body.velocity = Vector2.zero;

            if (shootingTimer >= shootingDuration)
            {
                isShooting = false;
                shootingTimer = 0.0f;
            }
        }
        if (isShootingCoolDown){
            shootingCoolDownTimer += Time.deltaTime;
            if (shootingCoolDownTimer >= shootingCoolDownDuration){
                isShootingCoolDown = false;
                shootingCoolDownTimer = 0.0f;
            }
        }
}

}

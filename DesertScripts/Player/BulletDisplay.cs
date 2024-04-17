using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet image prefab in the inspector
    public Transform bulletParent; // Parent object to hold instantiated bullet images
    public float bulletSpacing = 10f;
    public TopDownController player;

    private void Start(){
        UpdateBulletDisplay(player.numberOfBullets); // Initially, display 0 bullets
    }

    public void UpdateBulletDisplay(int numBullets)
    {
        // Clear previous bullets
        foreach (Transform child in bulletParent)
        {
            Destroy(child.gameObject);
        }

        // Display new bullets
        float totalWidth = numBullets * bulletPrefab.GetComponent<RectTransform>().rect.width + (numBullets - 1) * bulletSpacing;

        // Calculate starting position (far right)
        float startX = 200f;
        // Debug.Log("far right"+ startX);
        // Display new bullets
        for (int i = 0; i < numBullets && i < numBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent);
            RectTransform bulletRectTransform = bullet.GetComponent<RectTransform>();

            // Calculate position
            float xPos = startX - i * (bulletRectTransform.rect.width - bulletSpacing);
            bulletRectTransform.anchoredPosition = new Vector2(xPos, 0f);
        }
    }
}
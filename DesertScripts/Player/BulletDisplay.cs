using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform bulletParent; 
    public float bulletSpacing = 10f;
    public TopDownController player;

    private void Start(){
        UpdateBulletDisplay(player.numberOfBullets); 
    }

    public void UpdateBulletDisplay(int numBullets)
    {
        foreach (Transform child in bulletParent)
        {
            Destroy(child.gameObject);
        }

        float totalWidth = numBullets * bulletPrefab.GetComponent<RectTransform>().rect.width + (numBullets - 1) * bulletSpacing;

        float startX = 200f;
        // Debug.Log("far right"+ startX);
        for (int i = 0; i < numBullets && i < numBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent);
            RectTransform bulletRectTransform = bullet.GetComponent<RectTransform>();

            float xPos = startX - i * (bulletRectTransform.rect.width - bulletSpacing);
            bulletRectTransform.anchoredPosition = new Vector2(xPos, 0f);
        }
    }
}
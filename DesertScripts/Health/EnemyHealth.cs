using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
	public int currentHealth;

	public HealthBar healthBar;

    
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }


    

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
        healthBar.gameObject.SetActive(true);
		healthBar.SetHealth(currentHealth);
	}
}
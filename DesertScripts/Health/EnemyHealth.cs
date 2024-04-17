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

    // Update is called once per frame
    void Update()
    {
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
        //     if(!healthBar.isActiveAndEnabled)
        //     healthBar.gameObject.SetActive(true);
		// 	TakeDamage(5);
		// }
    }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
        healthBar.gameObject.SetActive(true);
		healthBar.SetHealth(currentHealth);
	}
}
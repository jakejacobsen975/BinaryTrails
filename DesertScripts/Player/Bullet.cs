using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 velocity = new Vector2(0.0f,0.0f);

    public GameObject player;

    public int damageAmount = 5;

    public EnemyHealth enemyHealth;

    // commented out code is from https://www.youtube.com/watch?v=vLIOpae8uAc&list=WL&index=25
    // void Update(){
    //     Vector2 currentPosition = new Vector2(transform.position.x,transform.position.y);
    //     Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

    //     Debug.DrawLine(currentPosition,newPosition,Color.red);

    //     RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

    //     foreach (RaycastHit2D hit in hits){
    //         GameObject other = hit.collider.gameObject;
            if(other != player){
                if(other.CompareTag("Enemy")){
                    EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damageAmount);
                    }
                    // Destroy(gameObject);
                    // Debug.Log(other.name);
                    // break;
                }
            }
            if (other.CompareTag("Object")){
                // Destroy(gameObject);
                Debug.Log(other.name);
                break;
            }



        }

        // transform.position = newPosition;


    }
}

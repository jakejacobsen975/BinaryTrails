using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traveler : MonoBehaviour
{
    public List<Sprite> sprites;

    float directionChangeTime = 5f;

    SpriteRenderer spriteRenderer;

    float tolerance = 0.5f;
    public bool isTalking = false;

    public DialogueTrigger dialogueTrigger;

   void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSpriteRandomly());
    }

    IEnumerator ChangeSpriteRandomly()
    {
        while (!isTalking)
        {
            yield return new WaitForSeconds(directionChangeTime);

            if (sprites.Count > 0)
            {
                int randomIndex = Random.Range(0, sprites.Count);
                spriteRenderer.sprite = sprites[randomIndex];
            }
            else
            {
                Debug.LogError("No sprites assigned to the list!");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 playerPosition = collision.gameObject.transform.position;
            Vector3 travelerPosition = transform.position;

            // Determine the position of the player relative to the traveler
            if (playerPosition.x < (travelerPosition.x - tolerance)){
                spriteRenderer.sprite = sprites[0];
            }
            else if (playerPosition.x > (travelerPosition.x + tolerance)){
                spriteRenderer.sprite = sprites[2];
            }
            else{
                spriteRenderer.sprite = sprites[1];
            }
            dialogueTrigger.TriggerDialogue();
            isTalking = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScenes : MonoBehaviour{
   
    public TopDownController player;
    public List<Enemy_AI> enemies;

    public string portalName;
    public PanelOpener panelOpenerWarning;

    public TextMeshProUGUI textMeshProElement;

    public Animator transition;
    private float cutsceneDuration = 5;

    bool skipRequested = false; 


    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && !CheckForEnemies() && !player.completedLevels.Contains(portalName)){
            player.body.velocity = Vector2.zero;
            StartCoroutine(PlayCutsceneAndLoadLevel());
            
            player.SaveGame();
        }
    }
    private IEnumerator PlayCutsceneAndLoadLevel() {
    transition.SetTrigger("start");
    string textCopy = textMeshProElement.text;
    textMeshProElement.text = "";
    
    

    foreach (char letter in textCopy.ToCharArray()) {
        textMeshProElement.text += letter;

        if(skipRequested){
            break;
        }

        yield return new WaitForSeconds(0.05f);
    }

    
    if (skipRequested) {
        textMeshProElement.text = textCopy;
    } else {
        yield return new WaitForSeconds(textCopy.Length * 0.05f);
    }

    SceneManager.LoadScene(portalName);
}
    

    bool CheckForEnemies()
    {
        foreach (Enemy_AI enemy in enemies)
        {
            if (enemy.gameObject.activeSelf) // Check if the enemy is active
            {
                float distance = Vector2.Distance(player.transform.position, enemy.transform.position);

                if (distance < 10f){
                    StartCoroutine(DisplayWarning());
                    return true;
                }
            }
        }
        
        return false;
    }
    IEnumerator DisplayWarning(){
    
    float animationDuration = 3f;
    float timer = 0.0f;

    while (timer < animationDuration)
    {
        panelOpenerWarning.OpenPanel();
        timer += Time.deltaTime;
        yield return null;
    }
    panelOpenerWarning.ClosePanel();
    }
    void Update() {

        if (Input.GetButtonDown("Jump")) {
            skipRequested = true;
        }
    }
}


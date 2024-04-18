using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadGame : MonoBehaviour{
    public GameObject noSave;
    public Animator transition;
    public Button button;
    private float cutsceneDuration = 5;

    
    void Start(){
        button.onClick.AddListener(checkForSave);
        
    }
    void checkForSave(){
        PlayerData data = SaveData.loadPlayer();
        if (data == null){
            noSave.SetActive(true);
            gameObject.SetActive(false);
        }else{
            StartCoroutine(PlayCutsceneAndLoadLevel());
        }
    }
    private IEnumerator PlayCutsceneAndLoadLevel(){
       
        transition.SetTrigger("start");
        
        yield return new WaitForSeconds(cutsceneDuration);
        SceneManager.LoadScene("Desert");



    }
}

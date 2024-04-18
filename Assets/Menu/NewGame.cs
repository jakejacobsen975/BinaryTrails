using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewGame : MonoBehaviour{
    public Animator transition;

    public TopDownController player;
    private float cutsceneDuration = 5;

    public Button maleButton, femaleButton;

    public GameObject existingSave;

    public ControllerMenu controllerMenu;

    void Start(){
        maleButton.onClick.AddListener(OnMaleButtonClick);
        femaleButton.onClick.AddListener(OnFemaleButtonClick);
    }

    void OnMaleButtonClick(){
        StartGame(Gender.Male);
    }

    void OnFemaleButtonClick(){
        StartGame(Gender.Female);
    }
    public void StartGame(Gender gender){
        PlayerData data = SaveData.loadPlayer();
        player.gender = gender;

        if (data == null){
            player.transform.position = new Vector2(0,0);
            SaveData.SavePlayerData(player);
            StartCoroutine(PlayCutsceneAndLoadLevel());
        }else{
            controllerMenu.ExistingMenuActive();
            existingSave.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private IEnumerator PlayCutsceneAndLoadLevel(){
       
        transition.SetTrigger("start");
        
        yield return new WaitForSeconds(cutsceneDuration);
        SceneManager.LoadScene("Desert");



    }
    public void overrideSave(){
        player.transform.position = new Vector2(0,0);
        player.completedLevels = new List<string>();
        SaveData.SavePlayerData(player);
        gameObject.SetActive(true);
        StartCoroutine(PlayCutsceneAndLoadLevel());
    }
}

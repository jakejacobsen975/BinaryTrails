using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalCheck : MonoBehaviour
{
    public List<multipleChoice> multipleChoices;
    bool AllCorrect = false;


    public string portalName;

    // Update is called once per frame
    void Update(){
        bool checkingAllAnswers = false;
        foreach( multipleChoice multipleChoice in multipleChoices){
            if(multipleChoice.correctAnswerChosen){
                checkingAllAnswers = true;
            }else{
                checkingAllAnswers = false;
                break;
            }
        }
        AllCorrect = checkingAllAnswers;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && AllCorrect){
            SceneManager.LoadScene("Desert");
            SaveData.SavePortalName(portalName);
        }
    }
}

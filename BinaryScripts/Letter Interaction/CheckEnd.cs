using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public List<LetterChecker> letterBlocks;

    bool AllCorrect ;

    public string portalName;


    // Update is called once per frame
    void Update(){
        AllCorrect = true;
        foreach(LetterChecker letter in letterBlocks){
            if(!letter.IsCorrectBlock){
                AllCorrect = false;
                break;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && AllCorrect){
            SceneManager.LoadScene("Desert");
            SaveData.SavePortalName(portalName);
        }
    }
}
// binary block interaction

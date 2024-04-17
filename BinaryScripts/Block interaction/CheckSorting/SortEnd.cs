using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SortEnd : MonoBehaviour
{
    public CheckAllNumber checkAllNumber;

    public string portalName;

    // Update is called once per frame
    void Update(){
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") && checkAllNumber.AllSortsCorrect){
            SceneManager.LoadScene("Desert");
            SaveData.SavePortalName(portalName);
        }
    }
}

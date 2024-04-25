using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    void Start(){
        button.onClick.AddListener(exitGame);
        
    }
    // commented code is from https://www.youtube.com/watch?v=zc8ac_qUXQY&list=WL&index=32
    // void exitGame(){
    //     Debug.Log("Player Quit");
    //     Application.Quit();
    // }
   
    
}

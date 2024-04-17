using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinningScript: MonoBehaviour{
    public GameObject winningMenu;

    public GameObject LevelLoader;

    public GameObject continueButton;
    

    public void NormalTime(){
            Time.timeScale = 1f;
    }
    
    
    public void winningMenuActive(){
        Time.timeScale = 0f;
        LevelLoader.SetActive(false);
        winningMenu.SetActive(true);
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign
        EventSystem.current.SetSelectedGameObject(continueButton);
    }
   
}

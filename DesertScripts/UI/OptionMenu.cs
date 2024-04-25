using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionMenu : MonoBehaviour{
    public GameObject optionsMenu;

    public GameObject optionsMenuFirstButton, controllerMenuFirstButton, keyboardInputsFirstButton, controllerInputsBackButton;
    
    // I learned how to set buttons for the controller from https://www.youtube.com/watch?v=SXBgBmUcTe0&list=WL&index=34&t=612s
    // but the logic is my own.
    void Update(){
        if(Time.timeScale == 1f && (Input.GetButton("Cancel") || Input.GetButton("Menu"))){
            optionsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void NormalTime(){
            Time.timeScale = 1f;
    }
    
    public void controllerMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(controllerMenuFirstButton);
    }
    public void optionsMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }
    public void KeyboardInputsMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(keyboardInputsFirstButton);
    }
    public void controllerInputsMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(controllerInputsBackButton);
    }
}

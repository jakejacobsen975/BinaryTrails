using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ControllerMenu : MonoBehaviour{
    public GameObject startFirstButton, selectionFirstButton, ExistingFirstButton, noSaveFirstButton;


    public void startMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(startFirstButton);
    }
    public void selectionMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(selectionFirstButton);
    }
    public void ExistingMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(ExistingFirstButton);
    }
    public void noSaveMenuActive(){
        // clear
        EventSystem.current.SetSelectedGameObject(null);

        //Reassign

        EventSystem.current.SetSelectedGameObject(noSaveFirstButton);
    }
}

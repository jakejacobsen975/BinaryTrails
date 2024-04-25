using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllorInput : MonoBehaviour
{
    public Button button;
    void Start(){
        button = gameObject.GetComponent<Button>();

        if (button == null){
            Debug.LogError("UI Button not found in the scene!");
        }
    }

    void Update(){
        bool isButtonPressed = false;
        if (Input.GetButtonDown("Jump")){
            button.onClick.Invoke();
            isButtonPressed = true;
        }
        //  if (!isButtonPressed)
        //     {
        //         // Freeze the game by setting Time.timeScale to 0
        //         Time.timeScale = 0f;
        //     }
        //     else
        //     {
        //         // Resume the game by setting Time.timeScale back to 1
        //         Time.timeScale = 1f;
        //     } chatGPT
    }
    public void SetTimeNormal(){
        Time.timeScale = 1f;
    }
}

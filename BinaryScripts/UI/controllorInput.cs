using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllorInput : MonoBehaviour
{
    public Button button;
    void Start()
    {
        // Attempt to find the UI button in the scene
        button = gameObject.GetComponent<Button>();

        // Check if the button was found
        if (button == null)
        {
            Debug.LogError("UI Button not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update(){
        bool isButtonPressed = false;
        if (Input.GetButtonDown("Jump"))
        {
            // Simulate a click event on the UI button
            button.onClick.Invoke();
            isButtonPressed = true;
        }
         if (!isButtonPressed)
            {
                // Freeze the game by setting Time.timeScale to 0
                Time.timeScale = 0f;
            }
            else
            {
                // Resume the game by setting Time.timeScale back to 1
                Time.timeScale = 1f;
            }
    }
    public void SetTimeNormal(){
        Time.timeScale = 1f;
    }
}

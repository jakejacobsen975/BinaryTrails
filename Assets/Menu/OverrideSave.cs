using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesButton : MonoBehaviour
{
    // Start is called before the first frame update
    public NewGame newGame;

    public Button button;

    // Update is called once per frame
    void Start(){
        button.onClick.AddListener(calloverridesave);
    }
    void calloverridesave(){
        newGame.overrideSave();
    }
}

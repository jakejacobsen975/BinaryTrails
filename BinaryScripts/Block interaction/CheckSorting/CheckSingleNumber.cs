using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNumber : MonoBehaviour
{
    public FirstMerge firstMerge;

    public FirstQuick firstQuick;

    public FirstSort firstBubble;

    public SecondMerge secondMerge;

    public SecondSort secondBubble;

    public List<NumberInteraction> numberInteractions;

    

    void Update(){
        for(int i = 0; i< numberInteractions.Count-1; i++){
            numberInteractions[i].isChangeable = false;
        }
        if(firstBubble.BlocksCorrect){
            numberInteractions[0].SetCorrect();
        }else{
            numberInteractions[0].SetIncorrect();
        }
        if(secondBubble.BlocksCorrect){
            numberInteractions[1].SetCorrect();
        }else{
            numberInteractions[1].SetIncorrect();
        }
        if(firstMerge.BlocksCorrect){
            numberInteractions[2].SetCorrect();
        }else{
            numberInteractions[2].SetIncorrect();
        }
        if(secondMerge.BlocksCorrect){
            numberInteractions[3].SetCorrect();
        }else{
            numberInteractions[3].SetIncorrect();
        }
        if(firstQuick.BlocksCorrect){
            numberInteractions[4].SetCorrect();
        }else{
            numberInteractions[4].SetIncorrect();
        }
    }
}



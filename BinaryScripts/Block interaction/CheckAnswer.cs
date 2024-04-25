using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class CheckAnswer : MonoBehaviour
{
    public List<BlockInteraction> binaryBlocks;

    public int asciiValue;
    
    
    bool BlocksCorrect = false;
    
    void Update(){
        List<int> currentBinarySequence = new List<int>();
        if(!BlocksCorrect){
            foreach (BlockInteraction binaryBlock in binaryBlocks)
            {
                int binaryState = binaryBlock.currentBlock;
                currentBinarySequence.Add(binaryState);
            }
            
            if(BinaryListToDec(currentBinarySequence) == asciiValue){
                
                foreach (BlockInteraction binaryBlock in binaryBlocks){
                    binaryBlock.gameObject.SetActive(false);
                }
                BlocksCorrect = true;
            }
        }
            
    }

    private int BinaryListToDec(List<int> sequence1){
        float result = 0.0f;
        int exponent = 0;

        for (int i = sequence1.Count - 1; i >= 0; i--){
            if (sequence1[i] == 1){
                result += Mathf.Pow(2, exponent);
            }

            exponent++;
        }
        
        return(int)math.round(result);
    }
    public bool IsBinaryCorrect(){
        return BlocksCorrect;
    }
}
// binary block interaction
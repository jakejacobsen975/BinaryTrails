using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMerge : MonoBehaviour
{
    public List<NumberInteraction> numberBlocks;
    public float resetDelay = 2f;

    private List<int> currentNumbersState;
    private int currentStep = 0;
    public bool BlocksCorrect = false;

    public List<List<int>> sortingSteps = new List<List<int>>
    {
        new List<int> { 7, 6, 8, 4, 1, 5 },
        new List<int> { 6, 7, 8, 4, 1, 5 },
        new List<int> { 6, 7, 8, 1, 4, 5 },
        new List<int> { 1, 7, 8, 6, 4, 5 },
        new List<int> { 1, 4, 8, 6, 7, 5 },
        new List<int> { 1, 4, 5, 6, 7, 8 }
    };
    BinaryPlayerMovement binaryMovement;

    void Start(){
        binaryMovement = FindAnyObjectByType<BinaryPlayerMovement>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!BlocksCorrect)
        {
            currentNumbersState = new List<int>();

            foreach (NumberInteraction numberBlock in numberBlocks)
            {
                currentNumbersState.Add(numberBlock.currentBlock);
            }

            if (ListsAreInSameOrder(currentNumbersState, sortingSteps[sortingSteps.Count-1]))
            {
                foreach (NumberInteraction numberBlock in numberBlocks)
                {
                    numberBlock.SetCorrect();
                }
                BlocksCorrect = true;
            }
            else
            {   
                List<int> nextStep = sortingSteps[currentStep+1];
                int both = 0;
                int greenCount = 0;
                for (int i = 0; i < numberBlocks.Count; i++){
                    
                    if (numberBlocks[i].currentState == NumberInteraction.blockState.GREEN ){
                        greenCount++;
                        if (numberBlocks[i].currentBlock != nextStep[i]){
                        both++;
                    }
                    }
                    if (both == 2){
                        currentStep++;
                        if (currentStep >= sortingSteps.Count){
                            currentStep = 0; // Reset to the first step if all steps are completed
                        }

                        // Update numberBlocks to the next step
                        for (int j = 0; j < numberBlocks.Count; j++){
                            numberBlocks[j].currentBlock = sortingSteps[currentStep][j];
                        }
                        foreach (NumberInteraction numberBlock in numberBlocks)
                            {
                                numberBlock.SetBlack();
                            }
                        break;
                    
                    }else if (both < 2 && greenCount >= 2){
                        currentStep = 0;
                        StartCoroutine(ResetBlocksAfterDelay());
                        // set all to incorrect and go back to step 1
                    }
                }


                
                
               
            }
        }
    }

    IEnumerator ResetBlocksAfterDelay()
    {
       

        foreach (NumberInteraction numberBlock in numberBlocks)
        {
            numberBlock.SetIncorrect();
        }
        binaryMovement.playerHealth.TakeDamage(10);

        yield return new WaitForSeconds(resetDelay);
        for (int j = 0; j < numberBlocks.Count; j++){
            numberBlocks[j].currentBlock = sortingSteps[0][j];
        }
        foreach (NumberInteraction numberBlock in numberBlocks)
        {
            numberBlock.SetBlack();
        }
    }

    bool ListsAreInSameOrder(List<int> list1, List<int> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false; // Lists have different lengths
        }

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false; // Elements at the same position are different
            }
        }

        return true; // All elements are the same and in the same order
    }
}

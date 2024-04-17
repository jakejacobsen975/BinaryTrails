using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveGun : MonoBehaviour
{
    public TopDownController player;
    
    public void giveGun(){
        player.hasGun = true;
         SaveData.SavePlayerData(player);
    }
    public void OpenClose(bool active){
        gameObject.SetActive(active);
    }
}

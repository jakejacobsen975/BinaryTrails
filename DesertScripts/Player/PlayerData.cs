using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[System.Serializable]
public class PlayerData {
   public List<string> completedLevels;

   public Gender gender;
   public float[] position;

   public bool hasGun;

   public PlayerData (TopDownController player){
        completedLevels = player.completedLevels;
        gender = player.gender;
        position = new float[2];
        hasGun = player.hasGun;

        position[0] = player.transform.position.x ;
        position[1] = player.transform.position.y - 2.0f;
   }
}

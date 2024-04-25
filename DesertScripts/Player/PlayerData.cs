using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
    // the commented out code is taken from https://www.youtube.com/watch?v=XOjd_qU2Ido&list=WL&index=30&t=2s

// [System.Serializable]
// public class PlayerData {
   public List<string> completedLevels;

   public Gender gender;
   // public float[] position;

   public bool hasGun;

   // public PlayerData (TopDownController player){
        completedLevels = player.completedLevels;
        gender = player.gender;
        position = new float[2];
        hasGun = player.hasGun;

        position[0] = player.transform.position.x ;
        position[1] = player.transform.position.y - 2.0f;
   }
}

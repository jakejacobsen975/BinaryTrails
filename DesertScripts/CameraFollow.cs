using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this was taken from https://www.youtube.com/watch?v=ZBj3LBA2vUY&list=WL&index=23&t=2s
// however most games that I have looked at have the same code 
// but for honesty sake I will give him credit
// public class CameraFollow : MonoBehaviour
// {
//     private Vector3 offset = new Vector3(0f,0f,-10f);
//     private float smoothTime = 0.01f;
//     private Vector3 velocity = Vector3.zero;

//     [SerializeField] private Transform target;

//     // Update is called once per frame
//     void Update()
//     {
//         Vector3 targetPosition = target.position + offset;
//         transform.position = Vector3.SmoothDamp(transform.position,targetPosition, ref velocity, smoothTime);
//     }
// }

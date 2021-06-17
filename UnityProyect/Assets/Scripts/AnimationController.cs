using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]private Animator cameraAnim;

    public void MoveCameraToPlayer(){
        cameraAnim.SetTrigger("MoveCameraToPlayer");
    }
    public void MoveCameraToHome(){
        cameraAnim.SetTrigger("MoveCameraToHome");
    }
}

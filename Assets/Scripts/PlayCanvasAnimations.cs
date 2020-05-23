using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCanvasAnimations : MonoBehaviour
{
    public void PlayAnimationClip(string nameOfAnimation)
    {
        GetComponent<Animation>().Play(nameOfAnimation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompleter : MonoBehaviour
{
    void Start()
    {

        Animator animation = gameObject.GetComponent<Animator>();
        if (animation != null)
        {
            AnimationEvent animationEvent = new AnimationEvent();
            animationEvent.time = 1f;
            animationEvent.functionName = "InvokeOnCompleteSelf";

            animation.runtimeAnimatorController.animationClips[0].AddEvent(animationEvent);
        }        
    }

    [Header("On Complete")]

    public UnityEngine.Events.UnityEvent onComplete;

    public void InvokeOnCompleteSelf(int irrelevant)
    {
        if (onComplete != null)
        {
            onComplete.Invoke();
        }
    }
}

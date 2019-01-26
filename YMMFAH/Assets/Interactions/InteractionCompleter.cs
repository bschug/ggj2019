using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCompleter : MonoBehaviour
{
    public InteractionManager parentInteractionManager;

    public void InvokeOnComplete(int irrelevant)
    {
        if (parentInteractionManager.onComplete != null)
        {
            parentInteractionManager.onComplete.Invoke();
        }
    }
}

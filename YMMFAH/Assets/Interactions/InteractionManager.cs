using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler
{
    //------------------------
    //------------------------
    #region Initialisation

    [Header("Interaction Points")]

    public Camera cam;
    public GameObject interactionStart;
    public GameObject interactionEnd;
    
    private Vector3 interactionStartScreenspace;
    private Vector3 interactionEndScreenspace;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        // get the interaction points in screenspace
        interactionStartScreenspace = cam.WorldToScreenPoint(interactionStart.transform.position);
        interactionEndScreenspace = cam.WorldToScreenPoint(interactionEnd.transform.position);

        // set animator
        animator.speed = 0;

        // set on animation complete event
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.time = 1f;
        animationEvent.functionName = "InvokeOnComplete";

        myAnimation.clip.AddEvent(animationEvent);
        InteractionCompleter interactionCompleter = myAnimation.gameObject.GetComponent<InteractionCompleter>();
        if (interactionCompleter == null)
        {
            interactionCompleter = myAnimation.gameObject.AddComponent<InteractionCompleter>();
        }
        interactionCompleter.parentInteractionManager = this;

    }

    #endregion
    //------------------------
    //------------------------
    #region Input

    private const int numInputsToAverage = 5;
    private List<Vector3> lastInputs;
    private bool pointerDown = false;

    // Update is called once per frame
    void Update()
    {

        if (pointerDown)
        {
            // get pointer position
            Vector3 pointerPosition = Vector3.zero;
            if (Input.touchCount > 0)
            {
                Vector2 touchPos = Input.GetTouch(0).position;
                pointerPosition = new Vector3(touchPos.x, touchPos.y, 0);
            }
            else
            {
                pointerPosition = Input.mousePosition;
            }

            // add it to inputs
            lastInputs.Add(pointerPosition);
            while (lastInputs.Count > numInputsToAverage)
            {
                lastInputs.RemoveAt(0);
            }

            // don't continue if not enough inputs
            if (lastInputs.Count < numInputsToAverage)
            {
                return;
            }

            // get delta
            Vector3 delta = Vector3.zero;
            float sumWeights = 0;
            for (int i = 1; i < lastInputs.Count; i++)
            {
                float weight = Mathf.Max(0.5f, (float)i / (float)(lastInputs.Count - 1));
                delta += weight * lastInputs[i];
                sumWeights += weight;
            }

            delta = new Vector3(delta.x / sumWeights, delta.y / sumWeights, delta.z / sumWeights);
            delta = delta - lastInputs[0];

            // get angle
            float currentAngle = Vector3.Angle((interactionEnd.transform.position - interactionStart.transform.position).normalized, (lastInputs[lastInputs.Count - 1] - lastInputs[0]).normalized);
           

            // progress depending on angle and delta magnitude
            float angleModifier = Mathf.Max(0, (90f - currentAngle) / 90f);
            float progress = delta.magnitude * angleModifier;

            UpdateAnimation(progress);
            
            Debug.Log(delta.magnitude.ToString() + " " + angleModifier.ToString() + " " + progress.ToString());

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        UpdateAnimation(0);        Debug.Log("Pointer Up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastInputs = new List<Vector3>();
        pointerDown = true;
        Debug.Log("Pointer Down");
    }

    #endregion
    //------------------------
    //------------------------
    #region Animation

    [Header("Animation")]

    public Animation myAnimation;
    public Animator animator;
    public float progressModifier;
    private float animationProgress = 0f;

    private void UpdateAnimation(float progress)
    {
        animator.speed = progressModifier * progress;
    }

    #endregion
    //------------------------
    //------------------------
    #region On Complete

    [Header("On Complete")]

    public UnityEngine.Events.UnityEvent onComplete;
    
    #endregion
    //------------------------
    //------------------------
}

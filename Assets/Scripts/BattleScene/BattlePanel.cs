using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class BattlePanel : MonoBehaviour
{
    public const float SHOW_TIME = 0.1f;    //How long it takes to hide/show

    private CanvasGroup canvasGroup;

    private Vector3 shownScale;             //The scale when fully shown
    private float shownAlpha;               //The alpha when fully shown

    private Vector3 hiddenScale = Vector3.zero; //The scale when fully hidden
    private float hiddenAlpha = 0;          //The alpha when fully hidden

    private enum State
    {
        showing,
        shown,
        hiding,
        hidden
    }
    private State currentState = State.hidden;
    private float timer = 0f;


    //Events

    void Awake()
    {
        //Get the shown scale and alpha
        canvasGroup = GetComponent<CanvasGroup>();

        shownScale = transform.localScale;
        shownAlpha = canvasGroup.alpha;

        //Start in the hidden state
        timer = 0f;
        currentState = State.hidden;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = hiddenAlpha;
    }

    void Update()
    {
        //Animate
        if (currentState == State.hiding)
        {
            TransitionState(shownScale, hiddenScale, shownAlpha, hiddenAlpha, SHOW_TIME, State.hidden);
        }
        else if (currentState == State.showing)
        {
            TransitionState(hiddenScale, shownScale, hiddenAlpha, shownAlpha, SHOW_TIME, State.shown);
        }
    }

    //Interface

    public void Show()
    {
        //Show the panel
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        currentState = State.showing;
        timer = 0f;
    }
    
    public void Hide()
    {
        //Hide the panel
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        currentState = State.hiding;
        timer = 0f;
    }

    //Misc methods

    private void TransitionState(Vector3 startScale, Vector3 endScale, float startAlpha, float endAlpha, float duration, State nextState)
    {
        //Increment the timer
        timer += Time.deltaTime;
        float t = timer / duration;

        //Tween the scale and alpha
        transform.localScale = Vector3.Lerp(startScale, endScale, t);
        canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);

        //Change state when timer is past duration
        if (timer >= duration)
        {
            transform.localScale = endScale;
            canvasGroup.alpha = endAlpha;
            timer = 0f;

            currentState = nextState;
        }
    }
}

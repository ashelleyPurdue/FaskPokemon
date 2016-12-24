using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public float defaultFlashLength = 1f;       //How long to flash for, by default

    private List<Renderer> renderers = new List<Renderer>();
    private List<Graphic> uiGraphics = new List<Graphic>();

    private float countdown = 0;
    private bool visible = true;


    void Awake()
    {
        //Populate the renderer list
        Renderer[] myRenderers = GetComponents<Renderer>();
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        renderers.AddRange(myRenderers);
        renderers.AddRange(childRenderers);

        //Populate the uiGraphics list
        Graphic[] myUIStuff = GetComponents<Graphic>();
        Graphic[] childUIStuff = GetComponentsInChildren<Graphic>();

        uiGraphics.AddRange(myUIStuff);
        uiGraphics.AddRange(childUIStuff);
    }

    void Update()
    {
        //If we're flashing, toggle between visible and invisible
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            SetVisible(!visible);
        }

        //If we're not flashing, make it visible
        if (countdown <= 0)
        {
            SetVisible(true);
        }
    }


    //Interface

    public void StartFlashing()
    {
        //Starts the flashing animation
        StartFlashing(defaultFlashLength);
    }

    public void StartFlashing(float time)
    {
        //Starts flashing for the given amount of time
        countdown = time;
    }


    //Misc methods

    private void SetVisible(bool visible)
    {
        //Hide/show all children

        //Don't change anything if we're already in the right spot
        if (this.visible == visible)
        {
            return;
        }

        //Set visible
        this.visible = visible;

        //Apply to all renderers
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }

        //Apply to all UI things
        foreach (Graphic g in uiGraphics)
        {
            g.enabled = visible;
        }
    }
}

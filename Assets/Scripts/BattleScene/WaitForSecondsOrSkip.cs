using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsOrSkip : CustomYieldInstruction
{
    private static List<WaitForSecondsOrSkip> skipList = new List<WaitForSecondsOrSkip>();

    private static bool batchSkippingEnabled = false;
    private static bool batchSkip = false;

    private float countdown;
    
    public override bool keepWaiting
    {
        get
        {
            //Count down
            countdown -= Time.deltaTime;

            //Stop waiting if we're in a batch skip
            if (batchSkippingEnabled && batchSkip)
            {
                return false;
            }

            //Stop waiting if time is up
            if (countdown <= 0)
            {
                skipList.Remove(this);
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public static void Skip()
    {
        //Tell all instances to skip
        foreach (WaitForSecondsOrSkip a in skipList)
        {
            a.countdown = 0;
        }

        //Clear the list
        skipList.Clear();

        //If batch skipping is enabled, start a batch skip
        if (batchSkippingEnabled)
        {
            batchSkip = true;
        }
    }
    
    public static void BeginBatchSkip()
    {
        //Enables batch-skipping.
        //If a skip command is issued while batch-skipping is enabled, then ALL waits will be skipped until batch skipping is disabled again.

        batchSkippingEnabled = true;
        batchSkip = false;
    }

    public static void EndBatchSkip()
    {
        //Disables batch-skipping

        batchSkippingEnabled = false;
        batchSkip = false;
    }

    public WaitForSecondsOrSkip(float time)
    {
        countdown = time;
        skipList.Add(this);
    }
}

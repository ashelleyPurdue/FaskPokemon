using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsOrSkip : CustomYieldInstruction
{
    private static List<WaitForSecondsOrSkip> skipList = new List<WaitForSecondsOrSkip>();
    private float countdown;
    
    public override bool keepWaiting
    {
        get
        {
            //Count down
            countdown -= Time.deltaTime;

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
    }

    public WaitForSecondsOrSkip(float time)
    {
        countdown = time;
        skipList.Add(this);
    }
}

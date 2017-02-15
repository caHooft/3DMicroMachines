using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{ 
    public Text timeText;
    public Text lapCountText;
    public Text positionText;
    private bool finished;
    //private ShowPanels showPanels;
    private StartOptions startScript;
    private float startTimer = 0.01f;

    public int lapCount;

    public void Awake()
    {
        //showPanels = GameObject.Find("UIMicroMachines").GetComponent<ShowPanels>();
        startScript = GameObject.Find("UIMicroMachines").GetComponent<StartOptions>();
    }

    public void Update()
    {
        if(true)
        {

        }

        //checks if the boolean finished is true
        if (finished)
        {
            //when this is call
            return;
        }
        else
        {
            //sets start time to the time since the level is loaded
            startTimer = Time.timeSinceLevelLoad;
            string minutes = ((int)startTimer / 60).ToString();
            string seconds = (startTimer % 60).ToString("f2");
            timeText.text = minutes + ":" + seconds;
        } 
    }

    public void NextLap()
    {
        //increases lap count with one
        lapCount++;

        //makes the lap count visibel in the UI
        switch (lapCount)
        {
            case 2:
                lapCountText.text = ("Final Lap!");
                break;

            case 1:
                lapCountText.text = ("Second Lap");
                break;

            case 0:
                lapCountText.text = ("First Lap");
                break;
        }

        //if the lapcount is higher or equal to 3 call the finished function
        if (lapCount >= 3)
        {
            Finish();
            //lapCountText.text = ("Bonus Lap" + (lapCount - 2));
        }
    }

    public void Finish()
    {
        //sets the text color of timerText to green
        //showPanels.ShowEndGamePanel();
        startScript.EndGameScreen();
        
        finished = true;
    }
}

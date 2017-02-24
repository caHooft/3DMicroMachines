using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	private ShowPanels showPanels;						
	private bool isPaused;								
	private StartOptions startScript;					
	
	
	void Awake()
	{
        //Get ShowPanels components store them in showpanels
        showPanels = GetComponent<ShowPanels> ();
        //Get StartOptions component store them in startoptions
        startScript = GetComponent<StartOptions> ();
	}

	
	void Update () {

        //if cancel button is down and if the game is not paused also checks if not in main menu
        if (Input.GetButtonDown ("Cancel") && !isPaused && !startScript.inMainMenu) 
		{
			//pause the game
			DoPause();
            Cursor.visible = true;
        }
        //If the button is pressed and the game is paused and not in main menu
        else if (Input.GetButtonDown ("Cancel") && isPaused && !startScript.inMainMenu) 
		{
			//unpause the game
			UnPause ();
            Cursor.visible = false;
        }

    }


    public void DoPause()
	{
		//Set isPaused to true
		isPaused = true;

        //Makes the game and the game time stop.
        Time.timeScale = 0;

		//game time starts running normally again
		showPanels.ShowPausePanel ();
	}


	public void UnPause()
	{
		
		isPaused = false;

        //Returns tha game to normal the game starts playing again.
        Time.timeScale = 1;

        //call the HidePausePanel function
        showPanels.HidePausePanel ();
	}

}

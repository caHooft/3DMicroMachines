using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class StartOptions : MonoBehaviour
{
    private PlayMusic playMusic;
    //Reference to PlayMusic script
    private Pause pause;
    private float fastFadeIn = .01f;                                    //fade time
    private ShowPanels showPanels;                                      //reverence to script
    public int sceneToStart = 1;                                        //sets scene to start in
    public bool changeScenes = true;                                    //If true, load a new scene when Start is pressed.
    public bool changeMusicOnStart;

    public AnimationClip fadeColorAnimationClip;                        //Animation clip for fade
    [HideInInspector]public bool inMainMenu = true;                     //If true, pause button disabled in main menu     
    [HideInInspector]public Animator animColorFade;
    [HideInInspector]public Animator animMenuAlpha;
    [HideInInspector]public AnimationClip fadeAlphaAnimationClip;       //Reference to ShowPanels script


    void Awake()
	{
        //Get a reference to ShowPanels
        showPanels = GetComponent<ShowPanels> ();

        //Get a reference to PlayMusic
        playMusic = GetComponent<PlayMusic> ();

        pause = GetComponent<Pause>();
	}


	public void StartButtonClicked()
	{
        //selects single player scene
        sceneToStart = 1;
        Cursor.visible = false;
        //If changeMusicOnStart is true, fade out volume of music
        if (changeMusicOnStart) 
		{
			playMusic.FadeDown(fadeColorAnimationClip.length);
		}

        //If true start fade and switch scenes halfway
        if (changeScenes) 
		{
			//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
			Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

			//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
			animColorFade.SetTrigger ("fade");
		}
        showPanels.HideEndGamePanel();
        pause.UnPause();

    }

    public void StartButtonClickedTwoPlayers()
    {
        //selects Split Screen game scene
        sceneToStart = 2;
        Cursor.visible = false;
        //If changeMusicOnStart is true, fade out volume of music
        if (changeMusicOnStart)
        {
            playMusic.FadeDown(fadeColorAnimationClip.length);
        }

        //If true start fade and switch scenes halfway
        if (changeScenes)
        {
            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");
        }
        showPanels.HideEndGamePanel();
        pause.UnPause();

    }
    public void RetryButtonClicked()
    {
        Cursor.visible = false;
        //If changeMusicOnStart is true, fade out volume of music
        if (changeMusicOnStart)
        {
            playMusic.FadeDown(fadeColorAnimationClip.length);
        }

        //If true start fade and switch scenes halfway
        if (changeScenes)
        {
            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");
        }

        //If changeScenes is false, call StartGameInScene
        else
        {
            //Call the StartGameInScene function to start game without loading a new scene.
            StartGameInScene();
        }

        pause.UnPause();
    }

    public void RestartButton()
    {
        Cursor.visible = false;
        //If changeMusicOnStart is true, fade out volume of music
        if (changeMusicOnStart)
        {
            playMusic.FadeDown(fadeColorAnimationClip.length);
        }

        //If true start fade and switch scenes halfway
        if (changeScenes)
        {
            //Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
            Invoke("LoadDelayed", fadeColorAnimationClip.length * .5f);

            //Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
            animColorFade.SetTrigger("fade");
        }

        //If changeScenes is false, call StartGameInScene
        else
        {
            //Call the StartGameInScene function to start game without loading a new scene.
            StartGameInScene();
        }
        showPanels.HideEndGamePanel();
        pause.UnPause();
    }
    public void EndGameScreen()
    {
        Cursor.visible = true;
        pause.DoPause();

        showPanels.ShowEndGamePanel();
        Time.timeScale = 0;

    }

    //Once the level has loaded, check if we want Music
    void OnLevelWasLoaded()
	{
		//if changeMusicOnStart is true, call the PlayLevelMusic function of playMusic
		if (changeMusicOnStart)
		{
			playMusic.PlayLevelMusic ();
		}	
	}


	public void LoadDelayed()
	{
        //enable pause menu for in game
        inMainMenu = false;

        //Call HideMenu function in showPanels to hide the main menu
        showPanels.HideMenu ();

        //Load the selected scene
        SceneManager.LoadScene (sceneToStart);
	}

	public void HideDelayed()
	{
        //Call HideMenu function in showPanels to hide the main menu
        showPanels.HideMenu();
	}

	public void StartGameInScene()
	{
        //Pause button now works since we are no longer in Main menu.
        inMainMenu = false;

		//If changeMusicOnStart is true, fade out volume of music 
		if (changeMusicOnStart) 
		{
			//Wait until game has started, then play new music
			Invoke ("PlayNewMusic", fadeAlphaAnimationClip.length);
		}
		//Set trigger for animator to start animation fading out Menu UI
		animMenuAlpha.SetTrigger ("fade");
		Invoke("HideDelayed", fadeAlphaAnimationClip.length);
		
	}


	public void PlayNewMusic()
	{
        //Fade music 
        playMusic.FadeUp (fastFadeIn);
        //Play selected music
        playMusic.PlaySelectedMusic (1);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour
{    
    public AudioClip titleMusic;
    public AudioClip mainMenuMusic;
    public AudioMixerSnapshot volumeDown;
    public AudioMixerSnapshot volumeUp;
    private AudioSource musicSource;
    private float resetTime = .01f;             


    void Awake () 
	{
        //Getting Audiosource
        musicSource = GetComponent<AudioSource>();
        //PlayLevelMusic();

    }

	public void PlayLevelMusic()
	{
        //This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //case 0 is for the title scene music
            case 0:
                musicSource.clip = titleMusic;
                break;
            //case 1 is for teh single player game
            case 1:
                musicSource.clip = mainMenuMusic;
                break;
            //case 2 is for the two player game
            case 2:
                musicSource.clip = mainMenuMusic;
                break;
        }
        //Fade music for smoother music switch
        FadeUp(resetTime);
        //Play music
        musicSource.Play();
    }
	
	//When the game is played in the same scene as the main menu this function can be used to set the music accordingly
	public void PlaySelectedMusic(int musicChoice)
	{

		//This looks at the integer musicChoice to decide which music clip to play.
		switch (musicChoice) 
		{
            //case 0 is for the title scene music
            case 0:
			musicSource.clip = titleMusic;
			break;
            //case 1 is for the main menu music
            case 1:
			musicSource.clip = mainMenuMusic;
			break;
		}
		//Play the music
		musicSource.Play ();
	}

    //this function quickly fades up the volume
    public void FadeUp(float fadeTime)
    {
        //Fade in the music for smoother music switch
        volumeUp.TransitionTo(fadeTime);
    }

    //Call this function to fade the volume to silence
    public void FadeDown(float fadeTime)
    {
        //Fade out the music for smoother music switch
        volumeDown.TransitionTo(fadeTime);
    }
}


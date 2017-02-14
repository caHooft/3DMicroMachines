using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetAudioLevels : MonoBehaviour
{

	public AudioMixer mainMixer;                    //Used to hold a reference to the AudioMixer mainMixer


    //This function can be called and given a float to set the sfx volume (gameplay sounds)
    public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

    //This function can be called and given a float to set the music volume
    public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}
}

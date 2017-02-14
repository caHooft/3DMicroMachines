using UnityEngine;
using System.Collections;

public class QuitApplication : MonoBehaviour {

	public void Quit()
	{
		//If builded version of game 
	#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
	#endif

		//If running in editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
	#endif
	}
}

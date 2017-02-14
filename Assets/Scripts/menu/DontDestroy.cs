using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Start()
	{
		//Atached object is not destroyed when loading new scene
		DontDestroyOnLoad(this.gameObject);
	}

	

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
	public string[] levelNames = new string[2] {"StartScene", "MainScene" };

	static SceneManager s = null;
	// Use this for initialization
	void Start () {
		if (s == null) {
			s = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterTempleClick() {
		SteamVR_LoadLevel.Begin (levelNames[1]);
	}
    public void ExitTempleClick()
    {
        SteamVR_LoadLevel.Begin(levelNames[0]);
    }
}

using UnityEngine;
using System.Collections;



public class LevelManager : MonoBehaviour {
	
	void Start(){
		if (Application.loadedLevel == 0){
		Invoke("LoadNextLevel", 2.5f);
		}
	}
	
	public void LoadLevel(string name) {
		Application.LoadLevel(name);
	}
	
	public void QuitRequest() {
		Application.Quit();
	}
	public void LoadNextLevel() {
		Application.LoadLevel (Application.loadedLevel +1);
		}
 }
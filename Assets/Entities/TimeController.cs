using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script that will handle running and stopping the game for 
//tactical purposes/as well as dev/debugging purposes.
public class TimeController : MonoBehaviour {
	//Zoom buttons
	public Button play;
	public Button pause;

	public void pauseGameplay(){
		Time.timeScale = 0.0f;
	}

	public void playGameplay(){
		Time.timeScale = 1.0f;
	}
}

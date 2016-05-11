using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour {

	public Canvas can;

	public void close_planet_info(){
		can.enabled = false;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour {

	public float zoomSpeed;
	public float moveSpeed;

	//Limitations to how far out and how close the 
	//camera is allowed to zoom.
	private float maxZoom = 10;
	private float minZoom = 100;

	//Zoom buttons
	public Button zoomIn;
	public Button zoomOut;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//CalculateZoom ();
		moveCamera ();

		if(Input.GetAxis("Mouse ScrollWheel") != 0f){
			Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
		}
	}

	void CalculateZoom(){
		float curOrth = Camera.main.orthographicSize;
		float scrollInput = Input.GetAxis ("Mouse ScrollWheel");

		//Check if we are zooming out.
		if(Camera.main.orthographicSize >= 10 && Camera.main.orthographicSize < 101){
			if(scrollInput < 0){
				float newOrth = curOrth + zoomSpeed;
				Camera.main.orthographicSize = newOrth;
			}else if(scrollInput > 0){
				float newOrth = curOrth - zoomSpeed;
				Camera.main.orthographicSize = newOrth;
			}
		}
	}

	//Keybindings for moving the camera, as of now
	//does not have any impact on zoom as we are 
	//at a fixed angle.
	void moveCamera(){
		bool left = Input.GetKey (KeyCode.LeftArrow);
		bool right = Input.GetKey (KeyCode.RightArrow);
		bool up = Input.GetKey (KeyCode.UpArrow);
		bool down = Input.GetKey (KeyCode.DownArrow);

		if(left){
			transform.Translate(new Vector3(-moveSpeed*Time.deltaTime,0,0));
		}else if(right){
			transform.Translate(new Vector3(moveSpeed*Time.deltaTime,0,0));
		}else if(up){
			transform.Translate(new Vector3(0,moveSpeed*Time.deltaTime,0));
		}else if(down){
			transform.Translate(new Vector3(0,-moveSpeed*Time.deltaTime,0));
		}
	}

	//Function for zooming in the camera depending on how far or close
	//the camera already is.
	public void zoomCamOut(){
		Camera.main.orthographicSize = Camera.main.orthographicSize-10;
	}

	public void zoomCamIn(){
		Camera.main.orthographicSize = Camera.main.orthographicSize+10;
	}
}

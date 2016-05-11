using UnityEngine;
using System.Collections;

//Script that will handle rotating single entities such
//such as a sun,planet,moor or asteroid.
public class SingleRotator : MonoBehaviour {

	//How fast the planet rotates, should be 
	//determined by day length but not necessary.
	public float rotation_speed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate((Vector3.forward * Time.deltaTime)*rotation_speed);
	}
}

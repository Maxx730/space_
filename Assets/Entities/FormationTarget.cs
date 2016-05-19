using UnityEngine;
using System.Collections;

//THIS OBJECT WILL DISPLAY THE TARGET REDICULE AS WELL AS THE POSITION FOR
//EACH UNIT IN THE UNIT GROUPS MOVING TARGET.
public class FormationTarget : MonoBehaviour {
	void OnDrawGizmos(){
		 Gizmos.DrawWireSphere(transform.position, .25f);
	}
}

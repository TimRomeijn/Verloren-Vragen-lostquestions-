using UnityEngine;
using System.Collections;

public class gearRotation : MonoBehaviour {

	public float Speed;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(new Vector3(0, 0, 90) * Speed * Time.deltaTime );
	}
}

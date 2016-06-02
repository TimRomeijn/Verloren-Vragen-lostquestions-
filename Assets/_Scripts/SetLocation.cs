using UnityEngine;
using System.Collections;

public class SetLocation : MonoBehaviour 
{
	private void OnTriggerEnter(Collider col)
	{
		col.gameObject.transform.parent.GetComponent<PlayerInput> ().SetLastLocation (gameObject);
	}
}

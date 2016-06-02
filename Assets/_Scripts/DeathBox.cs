using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour 
{

	private void OnCollisionEnter(Collision col)
	{
		col.gameObject.GetComponent<PlayerInput> ().Respawn ();
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, "respawner", true	);
		Gizmos.color = Color.red;	
		Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class InvertCamera : MonoBehaviour 
{
	public bool invertCamera = false;
	
	public float culling = -1;
	
	private void Start()
	{
		enabled = invertCamera;
	}

	private void OnPreCull () 
	{
		GetComponent<Camera>().ResetWorldToCameraMatrix ();
		GetComponent<Camera>().ResetProjectionMatrix ();
		GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix * Matrix4x4.Scale(new Vector3 (-1, 1, 1));
	}
	 
	private void OnPreRender () 
	{
		GL.invertCulling = true;
	}
	 
	private void OnPostRender () 
	{
		GL.invertCulling = false;
	}
	
	public void InvertPlayer1()
	{
		//culling *= -1;
		invertCamera = !invertCamera;
		enabled = invertCamera;
	}
	
	public void InvertPlayer2()
	{
		//culling *= -1;
		enabled = !invertCamera;
		invertCamera = !invertCamera;
		enabled = invertCamera;
		enabled = !invertCamera;
	}
}

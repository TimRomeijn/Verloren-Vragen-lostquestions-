using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerInput))]
[RequireComponent (typeof (ConstantForce))]
[RequireComponent (typeof (Rigidbody))]
public class PlayerInfo : MonoBehaviour 
{
	public int playerId;
	public int score { get; private set; }
	public int jumpAmount { get; private set; }
	private Camera playerCamera;
	private PlayerInput playerInput;

	private void Start()
	{
		playerCamera = GetComponentInChildren<Camera> ();
		playerInput = GetComponent<PlayerInput> ();
	}

	public void AddScore(int amount)
	{
		score += amount;
	}

	public void ToggleActive()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		//Debug.Log ("Changed State");
		playerCamera.enabled = !playerCamera.enabled;
		playerInput.enabled = !playerInput.enabled;
	}

	public void SetJumps(int amount)
	{
		jumpAmount = amount;
	}

	public void UseJump()
	{
		jumpAmount--;
	}

	public bool CanJump()
	{
		if (jumpAmount > 0)
			return true;

		return false;
	}


}

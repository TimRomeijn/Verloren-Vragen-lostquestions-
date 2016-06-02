using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
	public enum InputState
	{
		idle,
		walking,
		jumping,
		dead
	}

	private	DiceManager 	_diceManager	= null; 
	public	DiceManager 	diceManager
	{
		get
		{
			if (_diceManager == null)
			{
				_diceManager = Hierarchy.GetComponentWithTag<DiceManager>("DiceManager");
			}
			return _diceManager;
		}		
	}  

	private	PlayerManager 	_playerManager	= null; 
	public	PlayerManager 	playerManager
	{
		get
		{
			if (_playerManager == null)
			{
				_playerManager = Hierarchy.GetComponentWithTag<PlayerManager>("PlayerManager");
			}
			return _playerManager;
		}		
	} 

	private Animation 	animationRoot;
	
	private InputState 	inputState,
						lastState;

	public GameObject 	lastPlatform { get; private set;}

	public List<string> idleAnimations 			= new List<string> ();
	
	public int 			randomSeed 			= 0,
						movementSpeed 		= 0,
						jumpSpeed 			= 0;

	public string 		walkAnim			= "",
						deathAnim			= "",
						jumpAnim			= "";

	public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

	private Rigidbody rigidBody;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		animationRoot = GetComponentInChildren<Animation> ();
		animationRoot [jumpAnim].layer = 1;
		Random.seed = randomSeed;
	}

	private void Update()
	{
		Move ();
		Triggers ();
		Animate ();
	}

	private void Move()
	{
		if (!diceManager.CanRoll ()) {
			//if (IsGrounded ())
		
			if (Input.GetButtonDown("Jump") && IsGrounded () && playerManager.CanJump ()) {	
				lastState = inputState;
				inputState = InputState.jumping;
				rigidBody.AddForce (Vector3.up * jumpSpeed, ForceMode.Impulse);
				playerManager.UseJump();
			}else if (Input.GetAxis ("Horizontal") > 0) {
				lastState = inputState;
				inputState = InputState.walking;
				Rotate (new Vector3 (0, 90, 0));
			}else if (Input.GetAxis ("Horizontal") < 0) {
				lastState = inputState;
				inputState = InputState.walking;
				Rotate (new Vector3 (0, 270, 0));
			}else
			{
				lastState = inputState;
				inputState = InputState.idle;
			}

			rigidBody.velocity = (new Vector3 (Input.GetAxis ("Horizontal") * movementSpeed, rigidBody.velocity.y, rigidBody.velocity.z));
		}
	}

	private void Animate()
	{
		switch (inputState) 
		{
			case InputState.idle:
				if(lastState == InputState.walking)
				{
					animationRoot.CrossFade(idleAnimations[Random.Range(0, idleAnimations.Count)]);
				}
				else
					animationRoot.CrossFadeQueued(idleAnimations[Random.Range(0, idleAnimations.Count)]);
				break;
			case InputState.walking:
				animationRoot.CrossFade(walkAnim); 
				break;
			case InputState.jumping:
				animationRoot.CrossFade(jumpAnim);
				break;
			case InputState.dead:
				animationRoot.CrossFade(deathAnim);
				break;
		}

		Debug.DrawRay(transform.position, -transform.up, Color.red, 0.5f);
	}

	private void Triggers()
	{
		if (Input.GetButton ("EndTurn") && !diceManager.CanRoll()) {
			playerManager.NextPlayer ();
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	private void Rotate(Vector3 angle)
	{
		animationRoot.transform.parent.transform.rotation = Quaternion.Euler(angle);
	}

	private bool IsGrounded()
	{
		//		Debug.Log(distanceToGround);
		return Physics.Raycast(transform.position, -transform.up, 0.5f);
	}

	public void SetLastLocation(GameObject obj)
	{
		lastPlatform = obj;
	}

	public void Respawn()
	{
		rigidBody.isKinematic = true;
		transform.position = new Vector3(lastPlatform.transform.position.x, lastPlatform.transform.position.y + 2, lastPlatform.transform.position.z);
		inputState = InputState.idle;
		playerManager.NextPlayer ();
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
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

	public int currentPlayer { get; private set; }
	private int playerAmount;

	public List<PlayerInfo> players = new List<PlayerInfo>();

	private void Start()
	{
		SetPlayerAmount (players.Count);

		//players = Hierarchy.GetComponentsWithTag<PlayerInfo> ("Player");
	}

	public void SetPlayerAmount(int amount)
	{
		playerAmount = amount;
		currentPlayer = 0;
	}

	public void NextPlayer()
	{
//		Debug.Log ("Changing");
		players [currentPlayer].SetJumps (0);
		players [currentPlayer].ToggleActive ();

		currentPlayer++;
//		Debug.Log ("Changed player");

		if (currentPlayer > (playerAmount - 1)) 
		{
//			Debug.Log("Turn Over");
			currentPlayer = 0;
		}

		diceManager.ResetDice ();
		players [currentPlayer].ToggleActive ();
	}

	public void AddScore(int amount)
	{
		players[currentPlayer].gameObject.GetComponent<PlayerInput>().lastPlatform.GetComponent<QuestionPlatform>().NewQuestion();
		players [currentPlayer].AddScore (amount);
		NextPlayer ();
	}

	public int JumpsLeft()
	{
		return players [currentPlayer].jumpAmount;
	}

	public bool CanJump()
	{
		return players [currentPlayer].CanJump ();
	}

	public void SetJumps()
	{
		diceManager.RollDie ();
		players [currentPlayer].SetJumps(diceManager.RolledAmount());
	}

	public void UseJump()
	{
		players [currentPlayer].UseJump ();
	}

	public List<PlayerInfo> GetPlayers()
	{
		return players;
	}
}

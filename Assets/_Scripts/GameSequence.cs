using UnityEngine;
using System.Collections;

public class GameSequence : MonoBehaviour 
{
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

	
	// Update is called once per frame
	void Update () {
	/*	if (!playerManager.CanJump () && !diceManager.CanRoll()) {
			playerManager.NextPlayer();
		}*/
	}
}

using UnityEngine;
using System.Collections;

public class DiceManager : MonoBehaviour 
{
	private	DiceInterface 	_diceInterface = null; 
	public	DiceInterface 	diceInterface
	{
		get
		{
			if (_diceInterface == null)
			{
				_diceInterface = GetComponent<DiceInterface>();
			}
			return _diceInterface;
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

	public enum DiceType
	{
		singleDie,
		doubleDie
	}

	public DiceType diceType = DiceType.singleDie;

	private int rolledAmount;

	private bool rolledThisTurn = false;

	public Coroutine RollDie()
	{
		switch(diceType)
		{
			case DiceType.singleDie:
				rolledAmount = Random.Range(1, 6);
				break;
			case DiceType.doubleDie:
				int die1 = Random.Range(1, 6);
				int die2 = Random.Range(1, 6);
				rolledAmount =  die1 + die2;
				break;
		}

		return diceInterface.StartCoroutine ("DiceAnimation");
	}

	public void Rolled()
	{
		rolledThisTurn = true;
	}

	public bool CanRoll()
	{
		if (!rolledThisTurn)
			return true;

		return false;
	}

	public int RolledAmount()
	{
		return rolledAmount;
	}

	public void ResetDice()
	{
		rolledAmount = 0;
		rolledThisTurn = false;
	}
}

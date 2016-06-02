using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPlatform : MonoBehaviour 
{
	private	QuestionManager 	_questionManager	= null; 
	public	QuestionManager 	questionManager
	{
		get
		{
			if (_questionManager == null)
			{
				_questionManager = Hierarchy.GetComponentWithTag<QuestionManager>("QuestionManager");
			}
			return _questionManager;
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

	private QuestionManager.Question activeQuestion;
	public string element = "QUESTIONICON";

	private Vector2 screenOffset = new Vector2(40, 180);

	private void Start()
	{
		activeQuestion = questionManager.RandomQuestion ();
	}

	public void NewQuestion()
	{
		activeQuestion = questionManager.RandomQuestion ();
	}

	public void OnGUI()
	{
		GUI.depth = 99;
		screenOffset = new Vector2 (GUIMaster.GetElementRect (element).width / 3f, GUIMaster.GetElementRect (element).height * 2f);
		GUI.color = new Color(Color.white.r, Color.white.b, Color.white.b, .5f);
		GUI.DrawTexture(new Rect(Camera.main.WorldToScreenPoint(transform.position).x - screenOffset.x, Screen.height - Camera.main.WorldToScreenPoint(transform.position).y - screenOffset.y, GUIMaster.GetElementRect(element).width, GUIMaster.GetElementRect(element).height), questionManager.GetTexture(activeQuestion.difficulty));
		GUI.color = Color.white;
	}

	void OnTriggerEnter(Collider other) 
	{
		if(questionManager.activeQuestion == null && other.gameObject.transform.parent.GetComponent<PlayerInfo>().playerId == playerManager.currentPlayer && !diceManager.CanRoll())
		{
			questionManager.SetQuestion(activeQuestion);
			questionManager.StartTrigger();
		}
	}

	void OnTriggerExit(Collider other) 
	{
		if(other.gameObject.transform.parent.GetComponent<PlayerInfo>().playerId == playerManager.currentPlayer && !diceManager.CanRoll())
		{
			questionManager.SetQuestion(null);
			questionManager.StopTrigger();
		}
	}
}

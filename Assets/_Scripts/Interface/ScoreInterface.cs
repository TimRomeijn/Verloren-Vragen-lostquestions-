using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreInterface : MonoBehaviour 
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

	public GUIStyle textStyle;
	private GUIStyle scaledTextStyle;

	public string 	backgroundRect;

	public List<string> playerRects = new List<string>();

	private void OnGUI()
	{
		for(int i = 0; i < playerManager.GetPlayers().Count; i++)
		{
			GUI.Label(GUIMaster.GetElementRect(playerRects[i]), "Player " + playerManager.GetPlayers()[i].playerId + ": " + playerManager.GetPlayers()[i].score, scaledTextStyle);
		}
	}

	private void Update()
	{
		scaledTextStyle = GUIMaster.ResolutionGUIStyle (textStyle);
	}
}

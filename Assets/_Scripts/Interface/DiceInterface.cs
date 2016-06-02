using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiceInterface : MonoBehaviour 
{
	[System.Serializable]
	public class DiceTexture
	{
		public int number;
		public Texture2D texture;

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

	public string 	firstDiceRect,
					secondDiceRect,
					diceTextRect,
					jumpsLeftRect,
					messageRect,
					messageButtonRect;

	public Texture2D currentDiceTexture;
	private Texture2D jumpsLeftDiceTexture;

	private DiceManager diceManager;

	public float 	rollAmounts = 0,
					delay = 0.05f;

	public GUIStyle textStyle,
					boxStyle;
	private GUIStyle scaledTextStyle,
						scaledBoxStyle;

	public List<DiceTexture> diceTextures = new List<DiceTexture>();

	private bool finished = false;

	private void Start()
	{
		diceManager = GetComponent<DiceManager> ();
	}

	private void OnGUI()
	{

		GUI.Label (GUIMaster.GetElementRect (diceTextRect), "You rolled:", scaledTextStyle);
		GUI.DrawTexture (GUIMaster.GetElementRect (firstDiceRect), currentDiceTexture);

		GUI.Label (GUIMaster.GetElementRect (jumpsLeftRect), "Jumps left:", scaledTextStyle);
		if (playerManager.JumpsLeft () > 0 && jumpsLeftDiceTexture != null)
			GUI.DrawTexture (GUIMaster.GetElementRect (secondDiceRect), jumpsLeftDiceTexture);
		else
			GUI.Label (GUIMaster.GetElementRect (secondDiceRect), "None", scaledTextStyle);

		if (diceManager.CanRoll ()) {
			GUI.Label (GUIMaster.GetElementRect (messageRect), "Klik hieronder om de dobbelsteen te rollen!", scaledBoxStyle);
			if (GUI.Button (GUIMaster.GetElementRect (messageButtonRect), "Rol!", scaledBoxStyle)) {
				playerManager.SetJumps();
			}
		}
	}

	private void Update()
	{
		scaledTextStyle = GUIMaster.ResolutionGUIStyle (textStyle);
		scaledBoxStyle = GUIMaster.ResolutionGUIStyle (boxStyle);

		if(playerManager.JumpsLeft() > 0 && finished)
			jumpsLeftDiceTexture = diceTextures [playerManager.JumpsLeft () - 1].texture;
	}

	private IEnumerator DiceAnimation()
	{
		finished = false;

		for (int i = 0; i < rollAmounts; i++) {
			currentDiceTexture = diceTextures [Random.Range (0, diceTextures.Count - 1)].texture;
			
			yield return new WaitForSeconds (delay);
		}

		currentDiceTexture = diceTextures [diceManager.RolledAmount() - 1].texture;

		finished = true;

		diceManager.Rolled();

		yield return null;
	}
}

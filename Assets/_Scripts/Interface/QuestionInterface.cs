using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionInterface : MonoBehaviour 
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

	public string 		backgroundRect,
						messageRect,
						messageButtonRect,
						questionRect;

	public List<string> answerRects = new List<string> ();

	public GUIStyle 	textStyle,
						buttonStyle;
	private GUIStyle	scaledTextStyle,
						scaledButtonStyle;

	public Texture2D 	backgroundTexture;

	private bool 		active = false,
						trigger = false,
				 		answered = false,
						correct = false,
						started = false;

	// Update is called once per frame
	void OnGUI () 
	{
		scaledTextStyle = GUIMaster.ResolutionGUIStyle (textStyle);
		scaledButtonStyle = GUIMaster.ResolutionGUIStyle (buttonStyle);

		if(trigger)
		{
			GUI.Label(GUIMaster.GetElementRect(messageRect), "Klik hieronder om een vraag te beandwoorden!", scaledButtonStyle);
			if(GUI.Button(GUIMaster.GetElementRect(messageButtonRect), "Klik Hier!", scaledButtonStyle))
			{
				started = true;
				ToggleActive();
				StopTrigger();
			}
		}
		else if(active)
		{
			if(backgroundTexture != null)
				GUI.DrawTexture(GUIMaster.GetElementRect(backgroundRect), backgroundTexture);

			if(started && !answered)
			{
				GUI.Label(GUIMaster.GetElementRect(questionRect), questionManager.activeQuestion.questionText, scaledTextStyle);

				for(int i = 0; i < questionManager.activeQuestion.answers.Count; i++)
				{
					if(GUI.Button(GUIMaster.GetElementRect(answerRects[i]), questionManager.activeQuestion.answers[i].answerText, scaledTextStyle))
					{
						if(questionManager.CheckAnswer(i))
						{
							answered = true;
							correct = true;
						}
						else
							answered = true;
					}
				}
			}


			if(answered && correct)
			{
				GUI.Label(GUIMaster.GetElementRect(messageRect), "Goed gedaan, dat was het juiste andwoord!", scaledTextStyle);
				if(GUI.Button(GUIMaster.GetElementRect(messageButtonRect), "Ga Verder", scaledButtonStyle))
				{
					answered = false;
					correct = false;
					active = false;
					started = false;
					questionManager.EndQuestion(true);
				}
			}
			else if(answered)
			{
				GUI.Label(GUIMaster.GetElementRect(messageRect), "Helaas heb je her verkeerde andwoord gekozen!", scaledTextStyle);
				if(GUI.Button(GUIMaster.GetElementRect(messageButtonRect), "Ga Verder", scaledButtonStyle))
				{
					answered = false;
					active = false;
					started = false;
					questionManager.EndQuestion(false);
				}
			}
		}
	}

	public void StartTrigger()
	{
		trigger = true;
	}

	public void StopTrigger()
	{
		trigger = false;
	}

	public void ToggleActive()
	{
		active = !active;
	}
}

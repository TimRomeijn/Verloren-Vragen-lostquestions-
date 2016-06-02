using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour 
{
	private	QuestionInterface 	_questionInterface	= null; 
	public	QuestionInterface 	questionInterface
	{
		get
		{
			if (_questionInterface == null)
			{
				_questionInterface = Hierarchy.GetComponentWithTag<QuestionInterface>("QuestionInterface");
			}
			return _questionInterface;
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

	public enum QuestionType
	{
		engels,
		geschiedenis,
		rekenen,
		spelling,
		topografie
	}

	[System.Serializable]
	public class QuestionAnswer
	{
		public string answerText;
	}

	[System.Serializable]
	public class Question
	{
		public int correctAnswer = 0;
		public QuestionType difficulty = QuestionType.engels;
		public string questionText;
		public List<QuestionAnswer> answers = new List<QuestionAnswer>();
	}

	[System.Serializable]	
	public class QuestionTexture
	{
		public QuestionType type;
		public Texture2D texture;
	}

	public Question activeQuestion {get; private set;}

	public List<Question> questions = new List<Question>();
	public List<QuestionTexture> textures = new List<QuestionTexture>();
	private Dictionary<QuestionType, Texture2D> textureDic = new Dictionary<QuestionType, Texture2D>();

	private void Start()
	{
		foreach(QuestionTexture tex in textures)
		{
			textureDic.Add(tex.type, tex.texture);
		}
	}

	public void SetQuestion(Question question)
	{
		activeQuestion = question;
	}

	public Question RandomQuestion()
	{
		return questions[Random.Range(0, questions.Count)];;
	}

	public Texture2D GetTexture(QuestionType type)
	{
		return textureDic[type];
	}

	public void StartTrigger()
	{
		questionInterface.StartTrigger();
	}

	public void StopTrigger()
	{
		questionInterface.StopTrigger();
	}

	public bool CheckAnswer(int answer)
	{
		if (activeQuestion.correctAnswer == answer) 
		{
			return true;
		}

		return false;
	}

	public void EndQuestion(bool correct)
	{
		activeQuestion = null;
		if (correct)
			playerManager.AddScore (3);
		else
			playerManager.AddScore (0);
	}
}

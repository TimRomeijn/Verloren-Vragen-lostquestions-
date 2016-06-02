using UnityEngine;
using System.Collections;

public class MenuInterface : MonoBehaviour 
{
	public string logoElement,
				  level1Button,
				  level2Button;

	public Texture2D logo;

	public GUIStyle buttonStyle;
	private GUIStyle scaledButtonStyle;

	private void OnGUI()
	{
		scaledButtonStyle = GUIMaster.ResolutionGUIStyle (buttonStyle);

		GUI.DrawTexture (GUIMaster.GetElementRect (logoElement), logo);

		if (GUI.Button (GUIMaster.GetElementRect (level1Button), "Robot Level", scaledButtonStyle)) 
		{
			Application.LoadLevel("DemoLevel");
		}
		if (GUI.Button (GUIMaster.GetElementRect (level2Button), "Nature Level", scaledButtonStyle)) 
		{
			Application.LoadLevel("DemoLevel_Nature");
		}
	}
}

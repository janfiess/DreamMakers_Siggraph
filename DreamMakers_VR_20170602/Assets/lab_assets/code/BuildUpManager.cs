using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUpManager : MonoBehaviour 
{
	public List<BuildUp> builder = new List<BuildUp>();
	Global_Feedback feedbackManager;

	void Start () 
	{
		feedbackManager = GetComponent<Global_Feedback>();
	}



	public void BuildUpLabReal()
	{
		feedbackManager.OnRiseLab ();
		for (int i = 0; i < builder.Count; i++) 
		{
			builder [i].StartBuildUp ();
		}
	}
}

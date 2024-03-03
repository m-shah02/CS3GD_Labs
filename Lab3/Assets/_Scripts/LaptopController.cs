using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopController : MonoBehaviour 
{

  [SerializeField]
	private Text timeText;
	
	// Update is called once per frame
	void Update () 
	{
		float t = Time.timeSinceLevelLoad;
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
		timeText.text = string.Format("{0:D2}:{1:D2}", mins, rest);
	}
}

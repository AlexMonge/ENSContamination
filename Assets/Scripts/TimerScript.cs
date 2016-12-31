using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour {

	public Text timerText;
	private float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.time - startTime;

		string minutes = ((int) t / 60).ToString();
		string minutes2 = (minutes.Length == 1) ? "0" + minutes : minutes;
		string seconds = (t % 60).ToString("f0");
		string seconds2 = (seconds.Length == 1) ? "0" + seconds : seconds;

		timerText.text = minutes2 + ":" + seconds2;
	}
}

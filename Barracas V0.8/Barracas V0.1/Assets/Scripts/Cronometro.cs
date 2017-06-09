using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cronometro : MonoBehaviour {

	public  float totalTime = 120;
	public  bool  countDown = false;
	public  Text  cronometer;
	private float timer;
	private int   sign;

	void Start () {
		if (countDown) {
			timer = totalTime;
			sign  = -1;
		} else {
			timer = 0;
			sign  = 1;
		}
	}
	

	void Update () {
		timer += sign * Time.deltaTime;
		int minutes = (int)(timer / 60);
		int seconds = (int)(timer % 60);
		cronometer.text = 
string.Format("{0:00}:{1:00}", minutes, seconds);


	}
}









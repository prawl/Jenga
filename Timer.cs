using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
public GUIStyle timerStyle;	 // Must be set to public.  This holds the style properties of the timer.  See the gameObject GUIObject > Font Size.  Imp
public float startTime = 30f;	 // Holds the initial time for the count down timer
private float restSeconds; 	 // Holds the calculation of the amount of time left on the clock
private int roundedRestSeconds; // Rounds it for formatting purposes	
private int displaySeconds; 	// Self-Explanatory
private int displayMinutes; 	// Self-Explanatory	
private string text; 			// Holds the formatted output of the time to be put in a GUI box	
private bool timerOn; 		 	// controls the start of count down timer
private AudioSource audio;      // Self-Explanatory 
private float iniTime;	
private bool hideTimer = false;
	
	// The purpose of this function is to imitate a setter for the startTime value.
	public float setStartTime (float input){
		
		startTime = input;
		return startTime;	
	}
	
	// The purpose of this function is to imitate a setter for the restSeconds value.
	public float setRestSeconds (float input){
		
		restSeconds = input;
		return restSeconds;
	}
	
	// The purpose of this function is to start displaying the timer on screen.
	public void turnTimerOn (){
		
		timerOn = true;
	}
	
	// The purpose of this function is to stop displaying the timer on screen.
	public void turnTimerOff (){
		
		timerOn = false;	
	}
	
	// The purpose of this function is to flag true when the timer is about to run out
	public bool runningOutOfTime (){
		
		if (restSeconds < 10){
			
			return true;	
		}
		else{
			
			return false;	
		}
	}
	
	// The purpose of this function is to flag true when the timer has run out.
	public bool outOfTime (){
		
	//	Debug.Log(restSeconds);
		if (restSeconds < 0){
			
			
			return true;	
		}
		else{
			
			return false;	
		}
	}
	
	// The purpose of this function is to reset the timer back to it's inital value when called
	public void resetTimer (){
		
		iniTime = Time.time;
	}
	
	// The purpose of this function is to return a formatted string of the current calculated time
	public string calcTime () {
		
		//Debug.Log ("timerOn = " + timerOn);
		
		if(timerOn){ // When activated, start counting down to zero
			
			float subtractMe =  (Time.time - iniTime);
		    restSeconds = startTime - subtractMe;
			//Debug.Log ( "guiTime " + guiTime + " = " + " Time.time " + Time.time + " - " + "startTime" + startTime);
			//Debug.Log ( "restSeconds " + restSeconds + " = " + " countDownSeconds " + countDownSeconds + " - " + "guiTime" + guiTime);
			//Debug.Log ( "restSeconds " + restSeconds + " = " + " startTime " + startTime + " - " + "subtractMe" + subtractMe);
				
			if (runningOutOfTime()){
				
				//StartAudio();
				//Debug.Log("Music start playing now");
			}
			if (outOfTime()){
						
				//Debug.Log("Time is over!");	
				//iniTime = Time.time;
				resetTimer();
				turnTimerOff();
			}
			//display the timer
			roundedRestSeconds = Mathf.CeilToInt(restSeconds);
			displaySeconds = roundedRestSeconds % 60;
			displayMinutes = roundedRestSeconds / 60; 	
			text = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds);	
			//Debug.Log (text);
			return text;
		}
		else{ // Timer is currently off, display a static 60 seconds until it's activated again
			resetTimer();
			displayMinutes = 0;
			displaySeconds = 0;
			text = string.Format ("{0:00}:{00:30}", displayMinutes, displaySeconds);	
			//Debug.Log ( "restSeconds " + restSeconds + " = " + " startTime " + startTime + " - " + "subtractMe" );
			//Debug.Log (text);
			return text;
		}	
	}
	
	// The purpose of this function is to count down from a specified number to zero.  The start value of the counter will be hardcoded
	public void displayGUITimer(){
	
		string text = calcTime();
		
		
		if (hideTimer == false)	GUI.Label (new Rect ((Screen.width/2),0, 400, 200), text, timerStyle);	
		
	}
	
	public void HideTimer (){
		
		hideTimer = true;
		
	}
	
	public void displayWaitTimer(){
		
		string text = "Waiting";		
		GUI.Label (new Rect ((Screen.width/2),0, 400, 200), text, timerStyle);	
	}
	
	// The purpose of this function is to start playing audio when the current player has less than 10 seconds left in their turn.  Note to self: Change creditsSong name.
	public void StartAudio(){
		
		audio = (AudioSource)gameObject.AddComponent("AudioSource");
		AudioClip creditsSong;
		creditsSong = (AudioClip)Resources.Load("Sounds/heartbeat") as AudioClip; // Resources.Load looks only in the resources dir
		
		audio.clip = creditsSong;
		audio.Play();	
	
	}
	
	
	
}
	
	
	
	

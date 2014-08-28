using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public Timer timer;     // timer handles all functionality of the in game GUI count down timer.
	public ObjectHandler obj;
    private DragRigidbody dragger;   
	public ButtonHandler playerTurn;
	public bool enabled;
	public bool runOnce;
	public bool waiting = false;
	public bool readyToStart = false;
	public float iniTime;
	public bool started = true;
	
	// Use this for initialization
	void Start () {
		
		playerTurn = GetComponent<ButtonHandler>();
		dragger = this.GetComponent<DragRigidbody>();
		timer = GetComponent<Timer>();
		obj = GetComponent<ObjectHandler>();
		timer.setStartTime(30.00f); // Initial value of the timer
		dragger.turnedOn = true;
		enabled = true;
		runOnce = true;
		float iniTime = Time.time;
		
		if ((Application.loadedLevel) == 1){
			
			timer.turnTimerOff();	
			timer.HideTimer();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (!waiting){ // The timer is not currently on the waiting phase & we want to wait for the player to click a block 
			
			//Debug.Log ("Enabled = " + enabled);
			 dragger.StartTurn (); // Transform the object x,y,z axis 
		
			if (Input.GetMouseButtonDown(0) && obj.detectGamePiece() ){ // When the mouse is clicked 
				
				//Debug.Log ("You are pressing the left mouse button down.");
				if (enabled) timer.turnTimerOn(); // Start players turn
			}
	
			if (timer.outOfTime()){ // Time is zero
				
				//Debug.Log ("Time is out I should be disabling everything, waiting and incrementing the player");
				enabled = false;       // Disallow the timer to start
				dragger.disableDrag(); // Disallow dragging of the game piece
				waiting = true;
				iniTime = Time.time;
			}
		}
		else{ // waiting
			
			if (!readyToStart){
				
				// This essentially waits 5 seconds game time before starting the next turn
				float captureTime = Time.time - iniTime;
				if ( captureTime > 5 ) readyToStart = true;
			}
			else{
				
				StartNextTurn();
			}
		}	
	}
	
	/*
	IEnumerator WaitFor(float delay){
		
		yield return new WaitForSeconds(delay); 
		readyToStart = true;
		//Debug.Log ("I waited 5 seconds!");
		//Debug.Log ("I waited for 10 seconds"); // Load the start of the game here 
		//startNextTurn(); // After wait allow player to take control
		
		// Move to the next player
	}
	*/
	
	// The purpose of this function is to reset flags and timer for the next players turn
	void StartNextTurn (){
		
		playerTurn.PlayerCounter();
		dragger.enableDrag();
		enabled = true;
		readyToStart = false;
		waiting = false;
		timer.setRestSeconds(30.0f);	
	}
	
	
	void OnGUI () {
		//Debug.Log ("I should be seen");
		//timer.displayGUITimer();
	
		if (enabled) timer.displayGUITimer();
		else timer.displayWaitTimer();
	}
}


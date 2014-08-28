using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour {
	
	public Timer time;
	public DelegateMenu menuFunction;
	public ObjectCollision collision;
	public ButtonHandler winner;
	public float screenHeight; // Need to calculate GUI button position based upon the height/width variables.
	public float screenWidth;
	public float buttonHeight;
	public float buttonWidth;
	public bool devsPlay;
	
	// Use this for initialization
	public void Start () {
		
		time = GetComponent<Timer>();	
		winner = GetComponent<ButtonHandler>();
		menuFunction = GetComponent<DelegateMenu>();
		collision.GetComponent<ObjectCollision>();
		screenHeight = Screen.height;
		screenWidth = Screen.width;

		buttonHeight = screenHeight * 0.15f;// originally 3
		buttonWidth = screenWidth * 0.25f; //originally 4
		
	}
	
	// The purpose of this function is to save return a string that will display on game end
	public string DisplayWinnerString (){
		
		if (winner.GetWinner() == -1){
			
			string output = "You lose!";	
			return output;
		}
		else{
			
			string output = "Player " + winner.GetWinner() +  " is the winner!";
			return output;
			}
	}
	
	public void SpecialEffect (){
		
		if((Application.loadedLevel) == 3 ) { // Space: lose gravity
			 	
			//Debug.Log ("Starting special effect: Gravity!");
			GameObject[] allBricks = GameObject.FindGameObjectsWithTag("Brick");
			foreach (GameObject obj in allBricks) {
				obj.rigidbody.useGravity = false;
			}
			time.turnTimerOff();	
			time.HideTimer();
		}
			
		if ( (Application.loadedLevel) == 2 ) { // Lost City: Nuclear explosion
				
			//Debug.Log ("Starting special effect: Explosion!");
			time.turnTimerOff();	
			time.HideTimer();
				
		}
				
		if (( Application.loadedLevel) == 1) { // To be determined
				
			//Debug.Log ("Starting special effect: TBD!");
			//time.HideTimer();
			devsPlay = true;
		}
	}
	
	public void OnGUI (){
		
		//Debug.Log("Are you getting here?");
		if (collision.CheckEndGame()){	// The game is flagged to end
	
			SpecialEffect();
			
			if (!devsPlay){
				
				GUI.Box (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight),  DisplayWinnerString() );	
				if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.62f, screenHeight * 0.46f, buttonWidth/3, 50), "Exit to main menu")){
	
					Application.LoadLevel("MainMenu");
					menuFunction.ReloadMenu();
					//menuFunction.ReloadMenu();
				}
			}	
		}	
	}
}

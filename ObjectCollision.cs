using UnityEngine;
using System.Collections;

public class ObjectCollision : MonoBehaviour {

	public DelegateMenu menuFunction;
	private static bool gameEnd = false;	
	
	

	// Use this for initialization
	void Start () {
		
			menuFunction = GetComponent<DelegateMenu>();
			ResetGame();		
	}
	
	
	// The purpose of this function is to detect if a game piece collides with the ground.  It is important to note that this script must be attached to each
	// object you want to check for a collision.  
	void OnCollisionEnter(Collision collision) {
		
		if(collision.transform.tag ==  "Ground"){ // Collision dectected?
			
			EndGame();
			
			//Debug.Log ("Game Over: A brick collided with the ground!");
			//Application.LoadLevel("MainMenu");
		}
	}
	
	// The purpose of ths function is to act as a getter for the endGame value
	public bool GetGameEnd (){
		
		return gameEnd;	
	}
	
	// The purpose of this function is to act as a setter for the endGame value
	public bool SetGameEnd(bool input){
		
		if (input = true){
			
			gameEnd = true;
			return gameEnd;
		}
		else{
			
			gameEnd = false;
			return gameEnd;
		}
			
	}
	
	// The purpose of this function is to set a flag off for the end game GUI
	public void ResetGame (){
		
		gameEnd = false;	
	}
	
	// The purpose of this function is to set a flag on for the end game GUI
	public void EndGame() {
		
		//Debug.Log ("Hello from EndGame function!");
		gameEnd = true;
	}
	
	public bool CheckEndGame (){
	
		if (gameEnd){ // Game is over
		
			return true;	
		}
		else{		  // Game is not over
		
			return false;
		}
		
	}
	/*void OnGUI (){
		
		//Debug.Log("Are you getting here?");
			//if (gameEnd){	
				//Time.timeScale = 0;
				
			Debug.Log("Here?");
					GUI.Box (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight), "Player is the winner!" );	
				if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.62f, screenHeight * 0.46f, buttonWidth/4, 50), "Exit to main menu")){
					
					menuFunction.ReloadMenu();
					//Debug.Log ("FUCK YOU");
					Application.LoadLevel("MainMenu");
					//menuFunction.ReloadMenu();
				}
			
		}
		
	}
		*/		
}

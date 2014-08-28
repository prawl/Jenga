using UnityEngine;
using System.Collections;

public class DelegateMenu : MonoBehaviour {
	
	private delegate void MenuDelegate();
	private MenuDelegate menuFunction;
	private float screenHeight; // Need to calculate GUI button position based upon the height/width variables.
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;
	public float buttonHeight2;
	public float buttonWidth2;
	public float buttonHeight3;
	public float buttonWidth3;
	private bool playerSelect = false;
	private bool menuSelect = true;
	private bool levelSelect = false;
	public static int numOfPlayers;
	public static bool enabled = false;
	
	void Start()
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		
		buttonHeight = screenHeight * 0.3f;
		buttonWidth = screenWidth * 0.4f;
		
		buttonHeight2 = screenHeight * 0.08f;
		buttonWidth2 = screenWidth * .09f;
		
		buttonHeight3 = screenHeight * .08f;
		buttonWidth3 = screenWidth * .09f;
		
		if (Time.time < 1 ){	
			enabled = true;
			//Debug.Log ("Enabled should be true");
		}
	}
	
	
	void OnGUI(){
		
		if (enabled) MenuController();

		//	Debug.Log ("I am not enabled");
	}
	
	
	void MenuController (){
	
		//Debug.Log ("I am enabled");
		
		if (enabled){ // Show GUI
		
			if (menuSelect){ // Main menu
				
				DisplayMenu();
			}
			else if (playerSelect){ // Player select
				
				DisplayPlayers();	
			}
			else if (levelSelect) { // Level select
				
				DisplayLevels();	
			}
		}
	}
	
	 public void DisplayMenu (){
		
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.1f, buttonWidth, buttonHeight), "Start Game")){
			
			playerSelect = true;
			menuSelect = false;
		}
		
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.5f, buttonWidth, buttonHeight), "Quit Game")){
		 
			Application.Quit ();	
		}
	}
	
	public void DisplayPlayers (){
		
		GUI.Box(new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.3f, buttonWidth, buttonHeight), "Select the number of players");
			
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.55f, screenHeight * 0.43f, buttonWidth2, buttonHeight2), "1 Player")){
			
			numOfPlayers = 1;
			//LoadLevel1();
			levelSelect = true;
			playerSelect = false;			
		}
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.70f, screenHeight * 0.43f, buttonWidth2, buttonHeight2), "2 Players")){
		
			numOfPlayers = 2;
			levelSelect = true;
			playerSelect = false;
			//LoadLevel1();
		}
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.85f, screenHeight * 0.43f, buttonWidth2, buttonHeight2), "3 Players")){
		
			numOfPlayers = 3;
			levelSelect = true;
			playerSelect = false;
			//LoadLevel1();
		}
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 1.0f, screenHeight * 0.43f, buttonWidth2, buttonHeight2), "4 Players")){
			
			numOfPlayers = 4;
			levelSelect = true;
			playerSelect = false;
			//LoadLevel1();
		}
	}
	
	public void DisplayLevels (){
		
		GUI.Box(new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.3f, buttonWidth, buttonHeight), "Select a level");
			
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.60f, screenHeight * 0.43f, buttonWidth3, buttonHeight3), "Developer Scene")){
			
			LoadLevel("Level1");		
		}
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.75f, screenHeight * 0.43f, buttonWidth3, buttonHeight3), "Destroyed City")){
		
			LoadLevel("destroyed_city");
		}
		if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.90f, screenHeight * 0.43f, buttonWidth3, buttonHeight3), "Lost in Space")){
			
			LoadLevel("LostInSpace");
		}
	}
	
	public void ReloadMenu (){
		
		//Debug.Log("Do you ever see me?");
		enabled = true;	
	}

	public void LoadLevel (string inputLevel){
		
		enabled = false;
		Application.LoadLevel(inputLevel);	
	}
	
	
	public int GetPlayerCount (){
	
		//Debug.Log (numOfPlayers);
		return numOfPlayers;
	}
}

using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {
	
	public ObjectCollision obj;
	public GUIStyle myStyle;	 // Must be set to public.  This holds the style properties of the timer.  See the gameObject GUIObject > Font Size.  Imp
	public GUIStyle myStyle2;
	public string rules;
	public string controls;
	public bool displayRules;
	public bool displayControls;
	public bool quitGame; 
	public GUITexture myGUITexture;	
	public static DelegateMenu player;
	public static int playerTurn;
	public int maxPlayers;
	private float screenHeight; // Need to calculate GUI button position based upon the height/width variables.
	private float screenWidth;
	public float buttonHeight;
	public float buttonWidth;
	public float buttonHeight2;
	public float buttonWidth2;
	
	// Use this for initialization
	void Start () {
		
		obj = GetComponent<ObjectCollision>();
		
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		
		buttonHeight = screenHeight * 0.15f;// originally 3
		buttonWidth = screenWidth * 0.25f; //originally 4
		
		buttonHeight2 = screenHeight * 0.08f;
		buttonWidth2 = screenWidth * 0.09f;
		
		player = GetComponent<DelegateMenu>();
		maxPlayers = player.GetPlayerCount();
		StartTime(); // Must be here to prevent a bug.
		playerTurn = 1;
	}
	
	// The purpose of this function is to package the rules string to be placed in a GUI text
	public void InitalizeRules (){
			
	 rules = " Game Rules \n\n" +
	 "Order \n" +
	 "Each player will take their turn sequentially as labeled on screen. \n\n" +
	 "Objective \n " +
	 "Each player must pull a wooden block from a row below the top row \n and replace it on the top of the tower in  the opposite direction of the \n" +
	 "blocks on top. Play continues until one player causes the tower to fall. \n The last  player to have placed a block on  top successfully is the winner." +
	 "\n\n" +
	 "Timing\n" +
	 "The timer will start when a player selects a block. The player will have 30 \n" +
	 "seconds to place the desired block on the top. \n Once the timer reaches zero, the current player" +
	 " loses control of the block \n and it moves on the next player. \n " +
	 "\n\n\n\n" +
	 "Press the primary mouse button to continue";
	}
	
	// The purpose of this function package the controls string to be placed in a GUI text.
	public void InitalizeControls (){
			
		controls = " Controls \n\n" +
		"Click and drag mouse on block to side it. [Carefully!] \n" +
		"Click and drag mouse on screen to rotate the camera \n" +
		"A key -- Rotate camera to the left \n" +
		"D key -- Rotate camera to the right \n" +
		"E key -- Pan camera vertically up \n" +
		"Q key -- Pan camera vertically down \n" + 
		"Clicking on a block + A key  -- Rotate block 90 degree left\n" +
		"Clicking on a block + D key  -- Rotate block 90 degree right\n" +
		"Clicking on a block + W key -- Pushes the block [Useful for middle pieces]\n" +
		"Clicking on a block + D key -- Pulls the block [Useful for middle pieces] \n" +
		"\n\n\n\n" +
	 	"Press the primary mouse button to continue";
	}
	
	// The purpose of this function is to pause the time scale of the game which emulates pausing the game.
	public void PauseTime (){
		
		Time.timeScale = 0;	
	}
	
	// The purpose of this function is to resume the time scale of the game.	
	public void StartTime (){
		
		Time.timeScale = 1;	
	}
	
	// The purpose of this function is to prompt the user with a confirmation box before quitting the game.
	public bool ConfirmQuit (){
		
		quitGame = true;
		return quitGame;
	}
	
	// The purpose of this function is to resume the game if the user decides not to quit
	public bool CancelQuit (){
		
		quitGame = false;
		return quitGame;
	}
	
	
	 // The purpose of this function is to display the controls of the game when called.
	public bool DisplayControls (){
		
		displayControls = true;
		return displayRules;
	}
	
	public bool RemoveControls (){
		
		displayControls = false;
		return displayRules;
	}

	// The purpose of this function is to display the rules of the game when called.
	public bool ShowRules (){
		
		displayRules = true;
		return displayRules;
	}
	
	public bool RemoveRules (){
		
		displayRules = false;
		return displayRules;
	}
	
	public int GetWinner(){
		
		if (maxPlayers == 1){ // Only one player, no need to report winner.
		
			return -1;
		}
		
		if ((playerTurn - 1) == 0 ){
			
			playerTurn = maxPlayers + 1;
			return 	playerTurn;
		}
		
		else return playerTurn - 1;
	}
	
	public int PlayerCounter (){
		
		if (playerTurn == maxPlayers){
		
			playerTurn = 1;
			return playerTurn;
		}
		else{
			
			playerTurn++;	
			//Debug.Log ("I should have just incremented.  I am " + playerTurn);
			return playerTurn;
		}
	}
	
	public void OnGUI(){
		
		if (!obj.CheckEndGame()){ // Not the end of the game display the current player
		
			if (playerTurn == 1){
				
				GUI.Label (new Rect ((Screen.width-100),0, 400, 200), "Player 1", myStyle);	
			}
			else if (playerTurn == 2){
				
				GUI.Label (new Rect ((Screen.width-100),0, 400, 200), "Player 2", myStyle);
			}
			else if (playerTurn == 3){
				
				GUI.Label (new Rect ((Screen.width-100),0, 400, 200), "Player 3", myStyle);
			}
			else if (playerTurn == 4){
				
				GUI.Label (new Rect ((Screen.width-100),0, 400, 200), "Player 4", myStyle);
			}
		}
		
		if (GUI.Button (new Rect(0, 0, 100, 50), "Quit Game") || Input.GetKeyDown(KeyCode.Escape)){
			
			ConfirmQuit();
		}
		
		if (GUI.Button (new Rect(0, 60, 100, 50), "Rules")){
			
			 ShowRules();
		}
		
		if (GUI.Button (new Rect(0, 120, 100, 50), "Controls")){
			
			 DisplayControls();
		}
		
		if (quitGame){ 
			
			Time.timeScale = 0;
			GUI.Box (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight), "Are you sure you want to quit?");
				
			if (GUI.Button (new Rect((screenWidth - buttonWidth) * 0.55f, screenHeight * 0.44f, buttonWidth2, buttonHeight2), "Yes")){
				
				player.ReloadMenu();
				Application.LoadLevel("MainMenu");
				//player.ShowMenu();
			}
			
			if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.68f, screenHeight * 0.44f, buttonWidth2, buttonHeight2), "No")){
		
				StartTime();
				CancelQuit();
			}
		}
		
		if (displayRules){
			
			InitalizeRules();
			PauseTime();
			GUI.Box (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight*2), rules);
		
			if (Input.GetMouseButton(0)){
				
				RemoveRules();
				StartTime();
			}
		}
		
		if (displayControls){
			
			InitalizeControls();
			PauseTime();
			GUI.Box (new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.4f, buttonWidth, buttonHeight*2), controls);
			
				if (Input.GetMouseButton(0)){
				
				RemoveControls();
				StartTime();
			}
		}
	}
}

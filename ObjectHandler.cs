using UnityEngine;
using System.Collections;

public class ObjectHandler : MonoBehaviour  {

	public Transform pickObj = null;
	public GameObject currentBrick = null;
	public RaycastHit hit;
	private float dist;
	private Vector3 newPos;
	//private float speed = 100; //Default camera speed
	private float mouseposX; 
	private float point;
	
	
	public GameObject setCurrentBrick (string tagName){
		
		if(  GameObject.FindGameObjectWithTag(tagName) == null ){
			
			currentBrick = null;
			return currentBrick;
		}
		else{
			
			currentBrick = 	GameObject.FindGameObjectWithTag(tagName);
			return currentBrick;
		}
	}
	
	// The purpose of this function is to act as a getter for the GameObject currentBrick
	public GameObject getCurrentBrick (){
		
		return currentBrick;	
		
	}
			
 	// This function returns true if the mouse is currently over a gameObject brick
	public GameObject detectGamePiece(){
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit; 
	    	if (Physics.Raycast(ray, out hit)){ // Mouse is currently over a gameObject
					
				if(hit.collider.tag == "Brick"){// gameObject tag 
					
					//Debug.Log("You are currently hovering over a game piece!");
					currentBrick = hit.transform.gameObject;
					return currentBrick;	
				}
				else{
				
					//Debug.Log("Nothing detected.");
					return null;	
				}	
			}
			else{
					
				return null;
			}
	}
	
	// The purpose of this function is to rotate the currently held brick 90 degrees to reorientate the brick
	public void BrickRotation (){
		
		if (Input.GetKey(KeyCode.LeftArrow)){
			
			//Debug.Log ("I should have rotated 90 Degress on the left arrow!");
			currentBrick.transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			
			//Debug.Log ("I should have rotated 90 Degress on the right arrow!");
			currentBrick.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
			
		}
		else if(Input.GetKey(KeyCode.A) && detectGamePiece()){
			
			//Debug.Log ("I should have rotated 90 Degress on the A key!");
			currentBrick.transform.rotation = Quaternion.Euler(90,0,0);
			
		}
		else if(Input.GetKey(KeyCode.D) && detectGamePiece()){
			
			//Debug.Log ("I should have rotated 90 Degress on the D key!");
			currentBrick.transform.rotation = Quaternion.Euler(90,90,0);	

		}
	}	
	
	
	// The purpose of this function is to change the current brick a different shade
	public void Highlight(){
		
		if (Input.GetMouseButtonDown(0) && detectGamePiece()){ // Mouse is clicked and currently hovering on a game piece
			
			//Debug.Log("Mouse button down!");
			currentBrick.renderer.material.shader = Shader.Find("Self-Illumin/Specular");
			
		}
	/*	if (Input.GetMouseButtonUp(0) && currentBrick != null){ // Mouse click is released after a valid brick was clicked
			
			//Debug.Log("Mouse button up!");			
			GameObject[] allBricks = GameObject.FindGameObjectsWithTag("Brick");
			
			foreach (GameObject obj in allBricks) {
				obj.renderer.material.shader = Shader.Find ("Diffuse");
				obj.rigidbody.freezeRotation = false;
			}
		*/	
			//currentBrick.renderer.material.shader = Shader.Find("Diffuse");
			//currentBrick.renderer.material.mainTexture = brickTexture;
			//Destroy(currentBrick.renderer.material);
			//currentBrick.renderer.material.mainTexture = brickTexture;
			//currentBrick.renderer.material.color = Color.blue;
			//currentBrick.renderer.material.mainTexture = brickTexture;
			//currentBrick.renderer.material.color = Color.clear;
			
			//currentBrick.renderer.material.color = Color.red;
		}	
	
	// The purpose of this function is to reset all bricks back to a normal diffuse shade.  NOTE:  You have to turn all the brick back even though only one brick changes.
	// If not you'll run into a bug where you can accidently hit mulitple blocks at a time.
	public void RemoveHighlight(){
		
		if (Input.GetMouseButtonUp(0) && currentBrick != null){ // Mouse click is released after a valid brick was clicked
			
			GameObject[] allBricks = GameObject.FindGameObjectsWithTag("Brick");
			
			foreach (GameObject obj in allBricks) {
			obj.renderer.material.shader = Shader.Find ("Diffuse");
			obj.rigidbody.freezeRotation = false;
			}	
		}		
	}
	
	/*
	public void grabBlock(){
		
	    if (Input.GetMouseButton(0)){ // if left button creates a ray from the mouse
	    	
			//var ray : RaycastHit = Camera.main.ScreenPointToRay(Input.mousePosition);
	    	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (!pickObj){ // if nothing picked yet...
				
	            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Brick"){
									
	                // if it's a rigidbody, zero its physics velocity
	                if (hit.rigidbody) hit.rigidbody.velocity = Vector3.zero;
					
	                pickObj = hit.transform; // now there's an object picked
					
	                // remember its distance from the camera
	                dist = Vector3.Distance(pickObj.position, Camera.main.transform.position);
	            }
	            else{
	     
					//camRotate();
	            }
	        }
	        else { // if object already picked...
				
				// Vector3 = RayHitCast
				//newPos = hit.point;
				
	           newPos = ray.GetPoint(dist); // REAL CODE transport the object
	           // mouseposX = hit.point.x;
				//pickObj.position.x = mouseposX;
				
				
				pickObj.position= newPos;   // REAL CODE to the mouse position 
				Debug.Log (pickObj.position);
	        }    
	    }
	    else { // when button released free pickObj
	        pickObj = null;
	    }
	}
*/	
	
	
	
	
}



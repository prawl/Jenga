using UnityEngine;
using System.Collections;
 
public class Cam : MonoBehaviour{
	
	public Transform target;
	public float distance = 30.0f;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	public float x = 0.0f;
	public float y = 0.0f;
	public float minDistance = 20f;
	public float maxDistance = 50f;	
	public float speed = 45.0f;
	public float target2 = 270.0f;	
	public bool mouseDown = false;		
	public ObjectHandler obj;
	public GameObject defaultFocus;  
	public float curX;	
	public bool disableCam = false;	
	public bool runOnce = true;	
	public float baseFOV;	

	// The purpose of this function is to focus the main camera on a gameObject so that it can smooth rotate around it.
	public void setCameraFocus (){
		
		defaultFocus = GameObject.FindGameObjectWithTag("DefaultFocus"); // Finds the main camera focal point
		target = defaultFocus.transform; // Sets the main camera to focus on the gameObject hidden in the jenga tower
	}
	
	// The purpose of this function is to capture the coordinates 
	public void GetCoords(){
		
		var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        // Make the rigid body not change rotation
        if (rigidbody) rigidbody.freezeRotation = true;
    }
	
	// The purpose of this function is to handle camera functionality such as rotating the camera with the mouse and keyboard, and zooming.
	public void MoveCamera(){
		
		if (target && camera){
		
			Zoom ();
			//ButtonZoom();
		}	
	
		if (Input.GetKey(KeyCode.E)){
			
			//Debug.Log("E was Pressed!");
			if (defaultFocus.transform.position.y < 45){ // Set max height at 45
		
				defaultFocus.transform.Translate(Vector3.up * Time.deltaTime * 10);
			}
		}
		else if (Input.GetKey(KeyCode.Q)){
			
			//Debug.Log("E was Pressed!");
			if (defaultFocus.transform.position.y > 15){ // Set max height at 45
			
				defaultFocus.transform.Translate(Vector3.down * Time.deltaTime * 10);
			}
		}
		
		if (Input.GetMouseButton(0)){
			
			obj = GetComponent<ObjectHandler>();
			
			// not touching a game piece should be able to rotate camera normally
			if(!obj.detectGamePiece() && disableCam == false){
				
				//Debug.Log("Camera moving part!");	
			
				if (target){
					
					x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
					y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
					y = ClampAngle(y, yMinLimit, yMaxLimit); 
					transform.rotation = Quaternion.Euler(y, x, 0);
					transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
				}
			}
			
			// touching a game piece should lock camera in place and freeze the bricks axis
			else{
				
				/*
				curX = obj.currentBrick.transform.position.x;
				
				if (runOnce){
				
				float upperBound = curX + 10;
				float lowerBound = curX - 10;
				runOnce = false;
				}
					
				
				if ( curX > upperBound ){
					
				Debug.Log("I should be far enough away to automove now!");
				}
				else if( curX < lowerBound){
					
					Debug.Log("I should be far enough away to automove now!");	
				}
						//Debug.Log ("curX = " + curX + "upperBound = " + upperBound + "lowerBound = " + lowerBound);
				
				*/
				disableCam = true;
				obj.currentBrick.rigidbody.freezeRotation = true;
				BrickRotation();
				//KeyboardRotation();
				// Once a brick is clear of all other bricks auto drag it above the top of the tower
				ButtonZoom();
				// lowers the distance to focus on mouse click
				//ZoomTest();
				//target = obj.currentBrick.transform; 
			//	Debug.Log("Hello there!");	
			}
			//disableCam = false
		}
		else{// Not currently holding the mouse button down
			disableCam = false;
			KeyboardRotation();
		}
	}
	
	//// The purpose of this fucntion is to increase/decrase the coordinate towards an aribtary point with the W & S key
	public void ButtonZoom(){
		
		if (Input.GetKey(KeyCode.W)){
			
			//Debug.Log("You pressed W!");
			
			distance -= 0.5f;
			
			//distance -= Input.GetAxis("Mouse ScrollWheel") * distance;
			distance = Mathf.Clamp(distance, minDistance, maxDistance);
		
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			 
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
		
			//obj.currentBrick.rigidbody.freezeRotation = true;
			//obj.currentBrick.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			//obj.currentBrick.rigidbody.freezeRotation = true;
		}
		else if (Input.GetKey(KeyCode.S)){
			
			distance += 0.5f;
			
			//distance -= Input.GetAxis("Mouse ScrollWheel") * distance;
			distance = Mathf.Clamp(distance, minDistance, maxDistance);
		
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			 
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
			//obj.currentBrick.transform.position -= transform.forward * transform.localScale.x;
			//obj.currentBrick.rigidbody.freezeRotation = true;
			//Debug.Log("You pressed W!");	
			//obj.currentBrick.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ ;
			//obj.currentBrick.rigidbody.freezeRotation = true;
		}
		else{
			//obj.currentBrick.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionZ;
			//obj.currentBrick.rigidbody.freezeRotation = true;
		}
	}

	// The purpose of this function is to rotate the camera using the keyboard commands a, d, left arrow and right arrow
	public void KeyboardRotation (){
		
		if (Input.GetKey(KeyCode.LeftArrow)){
			
			x += 150 * Time.deltaTime;
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			
			x -= 150 * Time.deltaTime;
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
			
		}
		else if(Input.GetKey(KeyCode.A)){
			
			x += 150 * Time.deltaTime;
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
			
		}
		else if(Input.GetKey(KeyCode.D)){
			
			x -= 150 * Time.deltaTime;
			transform.rotation = Quaternion.Euler(y, x, 0);
			transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
		}
	}	
	
		public void BrickRotation (){
		
		int test = 90;
		
		if (Input.GetKey(KeyCode.LeftArrow)){
			
			//Debug.Log ("I should have rotated 90 Degress on the left arrow!");
			obj.currentBrick.transform.rotation = Quaternion.Euler(90,0,0);
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			
			test += 90;
			//Debug.Log ("I should have rotated 90 Degress on the right arrow!");
			obj.currentBrick.transform.rotation =  Quaternion.Euler(90,90,0);
			
		}
		else if(Input.GetKey(KeyCode.A)){
			
			//Debug.Log ("I should have rotated 90 Degress on the A key!");
			obj.currentBrick.transform.rotation = Quaternion.Euler(90,0,0);
			
		}
		else if(Input.GetKey(KeyCode.D)){
			
			//Debug.Log ("I should have rotated 90 Degress on the D key!");
			obj.currentBrick.transform.rotation = Quaternion.Euler(90,90,0);	

		}
	}	
		
	// The purpose of this fucntion is to increase/decrase the coordinate towards an aribtary point
    public float ClampAngle(float angle, float min, float max){
    	
		if (angle < -360){
                
			angle += 360;
        }
        if (angle > 360){
                
			angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
	
	// The purpose of this function is to allow the ability to zoom in and out using the mouse wheel
	public void Zoom (){
		
		distance -= Input.GetAxis("Mouse ScrollWheel") * distance;
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
		
		y = ClampAngle(y, yMinLimit, yMaxLimit);
			 
		transform.rotation = Quaternion.Euler(y, x, 0);
		transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
	}
}

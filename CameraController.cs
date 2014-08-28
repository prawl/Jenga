using UnityEngine;
using System.Collections;
 
public class CameraController : MonoBehaviour{
	
	public Cam camera;  
	public ObjectHandler obj;
	
	void Start (){
		
		obj = GetComponent<ObjectHandler>();
	  	camera = GetComponent<Cam>(); 	
		camera.setCameraFocus();
		
    }
 
    void Update (){
		
		camera.MoveCamera();
		
		if (Input.GetMouseButtonDown(0)){
			
			obj.Highlight();	
		}
		if (Input.GetMouseButtonUp(0)){
			
		 	obj.RemoveHighlight();	
		}
	}
	
}
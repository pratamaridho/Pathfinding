using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {

	[SerializeField][Range(1,100)]			//seberapa cepat player bergerak
	private float speed = 10;

	private Vector3 targetPosition;			//dimana kita ingin pergi berjalan
	private bool isMoving;					//Beralih untuk memeriksa track jika kita ingin bergerak / tidak

	const int LEFT_MOUSE_BUTTON = 0;		//a more visual description of what the left mouse button code is moving


	// Use this for initialization
	void Start () {
		targetPosition = transform.position;	//set the target position to where we are at the Start
		isMoving = false;						//set out move toggle to false  
	}
	
	// Update is called once per frame
	void Update () {

		//if the player clicked on the screen, find out where
		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON)){
			setTargetPosition();
		}

		//if we are still moving, then move the player
		if(isMoving){
			MovePlayer();
		}
	
	}

	//Sets the target position we will travel too
	void setTargetPosition(){
		Plane plane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float point = 0f;

		if(plane.Raycast(ray, out point))
			targetPosition = ray.GetPoint(point);
		

		//set the ball to move
		isMoving = true;

	}

	//Moves the player in the right direction and also rotates them to look at the target position
	//When the player gets to the target position, stop them form moving
	void MovePlayer(){
		
		transform.LookAt(targetPosition);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

		//if we are at the target position, then stop moving
		if(transform.position == targetPosition)
			isMoving = false;

			Debug.DrawLine(transform.position, targetPosition, Color.red);
		
	}



}

using UnityEngine;
using System.Collections;


[DisallowMultipleComponent]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerMovementNavMesh : MonoBehaviour {

	private Vector3 targetPosition;		//where we want to travel to

	const int LEFT_MOUSE_BUTTON = 0;	//

	UnityEngine.AI.NavMeshAgent agent;


	void Awake(){
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

	}

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;	//set the target position to where we are at the Start
	}
	
	// Update is called once per frame
	//Detect the player input every frame
	void Update () {

		//if the player clicked on the screen, find out where
		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON))
			SetTargetPosition();

		MovePlayer();		
	
	}

	//Sets the target position we will travel too
	void SetTargetPosition(){

		Plane plane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float point = 0f;

		if(plane.Raycast(ray, out point))
			targetPosition = ray.GetPoint(point);
	}

	//MOves the player in the right direction and also rotates them to look at the target position.
	//When the player gets to the target position, stop them from moving

	void MovePlayer(){

		agent.SetDestination(targetPosition);

		Debug.DrawLine(transform.position, targetPosition, Color.red);
	}

}

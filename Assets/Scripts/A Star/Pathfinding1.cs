﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding1 : MonoBehaviour {


	public Transform seeker, target;

	Grid grid;

	void Awake(){
		grid = GetComponent<Grid>();
		
	}

	void Update(){
		FindPath(seeker.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 targetPos){

		Node starNode = grid.NodeFromWorldPoint	(startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		List<Node> openSet = new List<Node>();		//	tambahkan using.System.Collections.Generic
		HashSet<Node> closedSet = new HashSet<Node>();	//CLose
		openSet.Add(starNode);

		while(openSet.Count > 0){
			Node currentNode = openSet[0];	//node in OPen with the lowest fCost
			for(int i = 1; i < openSet.Count; i++){
				if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){
					currentNode = openSet[i];
				}
				
			}

			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if(currentNode == targetNode){			//ketika menemukan path 
				RetracePath(starNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(currentNode)){ 
			if(!neighbour.walkable || closedSet.Contains(neighbour)){
				continue;
			}

			int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
			if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
				neighbour.gCost = newMovementCostToNeighbour;
				neighbour.hCost = GetDistance(neighbour, targetNode);
				neighbour.parent = currentNode;

				if(!openSet.Contains(neighbour))
					openSet.Add(neighbour);
			}

		}

	}

		
}

	void RetracePath(Node starNode, Node endNode){
		 List<Node> path = new List<Node>();
		 Node currentNode = endNode;

		 while(currentNode != starNode){
			 path.Add(currentNode);
			 currentNode = currentNode.parent;
		 }
		 path.Reverse();

		 grid.path = path; 	//path dari kelas Grid

	}

	int GetDistance(Node nodeA, Node nodeB){
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if(dstX > dstY)
			return 14*dstY + 10*(dstX-dstY);
		return 14*dstX + 10*(dstY-dstX);

	}


}

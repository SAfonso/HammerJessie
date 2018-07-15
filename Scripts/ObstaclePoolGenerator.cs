using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolGenerator : MonoBehaviour {

	public GameObject obstaclePrefab;
	public int obsPoolSize = 5; 
	public float obsDistance;
	private GameObject[] obstacle;
	private Vector2 objectPoolPosition = new Vector2 (-10,-25);
	private int currentObs;
	// Use this for initialization
	void Start () {
		currentObs = 0;
		obstacle = new GameObject[obsPoolSize];
		for(int i = 0; i < obsPoolSize; i++)
        {
            //...and create the individual columns.
            obstacle[i] = (GameObject)Instantiate(obstaclePrefab, objectPoolPosition, Quaternion.identity);
        }
	}
	
	public void PutAnObstacle(GameObject floorFather){
		float xPos = floorFather.transform.position.x;
		Debug.Log(xPos);
		float yPos = floorFather.transform.position.y + obsDistance;
		Debug.Log("Estoy AQUII!!!");
		obstacle[currentObs].transform.position = new Vector2(xPos, yPos);
		obstacle[currentObs].transform.SetParent(floorFather.transform);
		currentObs++;
		if (currentObs >= obsPoolSize) 
        {
            currentObs = 0;
        }
	}

	public void QuitAnObstacle(GameObject child){
		child.transform.position = objectPoolPosition;
		child.transform.parent = null;
	}

	public Vector2 GetDefaultPos(){
		return objectPoolPosition;
	}
}

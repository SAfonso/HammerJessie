using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoolGenerator : MonoBehaviour {
	public GameObject coinPrefab;
	public int coinPoolSize = 5; 
	public float coinDistance;
	private GameObject[] coins;
	private Vector2 objectPoolPosition = new Vector2 (-15,-25);
	private int currentCoin;
	// Use this for initialization
	void Start () {
		currentCoin = 0;
		coins = new GameObject[coinPoolSize];
		for(int i = 0; i < coinPoolSize; i++)
        {
            //...and create the individual columns.
            coins[i] = (GameObject)Instantiate(coinPrefab, objectPoolPosition, Quaternion.identity);
        }
	}
	
	public void PutAcAT(GameObject floorFather){
		float xPos = floorFather.transform.position.x;
		float yPos = floorFather.transform.position.y + coinDistance;
		coins[currentCoin].transform.gameObject.SetActive(true);
		coins[currentCoin].transform.gameObject.GetComponent<Coin>().Init();
		coins[currentCoin].transform.position = new Vector2(xPos, yPos);
		coins[currentCoin].transform.SetParent(floorFather.transform);
		currentCoin++;
		if (currentCoin >= coinPoolSize) 
        {
            currentCoin = 0;
        }
	}

	public void QuitACat(GameObject child){
		child.transform.position = objectPoolPosition;
		child.transform.parent = null;
	}

	public Vector2 GetDefaultPos(){
		return objectPoolPosition;
	}
}

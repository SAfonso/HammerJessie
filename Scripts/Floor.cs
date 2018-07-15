using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floor : MonoBehaviour {

	[SerializeField]
	GameObject[] floorTiles;

	public Limites limite;

	Sprite floor;
	Sprite breakeable;

	private CoinPoolGenerator coinGenerator;

	[System.Serializable]
	public struct Limites{
		[Range(0,3)]
		public int min;
		[Range(2,5)]
		public int max;
	}



	public void SetLimite(int min,int max){
		limite.min = min;
		limite.max = max;
	}

	void Start(){
		floor = GetComponentInParent<BlockController>().floor;
		breakeable = GetComponentInParent<BlockController>().breakeable;
		coinGenerator = this.GetComponentInParent<CoinPoolGenerator>();
		SetLimite(0,5);
		SetHole(0);
	}

	public void SetHole(int randomCoin){
		System.Random rand = new System.Random();
		int hole = rand.Next(limite.min,limite.max);
		hole = CheckRandomHole(hole);
		int catInThisPos = rand.Next(1,5);
		int catWasPut = 0;
		CheckSecondsWalls();
		for (int i = 0 ; i< 5 ; i++){
			if(floorTiles[i].transform.childCount > 0){
				coinGenerator.QuitACat(floorTiles[i].transform.GetChild(0).gameObject);
			}
			if(i == hole){
				floorTiles[i].tag = "Breakeable";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = breakeable;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
			}else if(i>=limite.min && i<limite.max){
				floorTiles[i].tag = "Floor";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = floor;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
				if (randomCoin > 2 && catWasPut == catInThisPos){
					coinGenerator.PutAcAT(floorTiles[i].gameObject);
					catWasPut++;
				}
				catWasPut++;
			}else{
				floorTiles[i].tag = "Floor";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = floor;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
				if (CheckFloorPosition(floorTiles[i].gameObject)){
					this.transform.GetChild(9).gameObject.SetActive(true);
					this.transform.GetChild(9).transform.position = new Vector2(floorTiles[i].transform.position.x - 0.5f,this.transform.GetChild(9).transform.position.y);
				}else{
					this.transform.GetChild(8).gameObject.SetActive(true);
					this.transform.GetChild(8).transform.position = new Vector2(floorTiles[i].transform.position.x + 0.5f,this.transform.GetChild(8).transform.position.y);
				}
			}
		}
	}

	public bool CheckFloorPosition(GameObject thisFloor){
		if (thisFloor.transform.position.x < 0){
			return true;
		}else{
			return false;
		}
	}

	public void CheckSecondsWalls(){
		if (this.transform.GetChild(9).gameObject.activeSelf){
			this.transform.GetChild(9).gameObject.SetActive(false);
		}
		if (this.transform.GetChild(8).gameObject.activeSelf){
			this.transform.GetChild(8).gameObject.SetActive(false);
		}
	}

	public int CheckRandomHole(int hole){
		System.Random rand = new System.Random();
		int thisHole = hole;

		if (GameManager.instance.GetOldHole() == 6){
			GameManager.instance.SetOldHole(thisHole);
		}else {
			if (GameManager.instance.GetOldHole() == thisHole){
				int thisHoleCopy = rand.Next(limite.min,limite.max);
				thisHole = CheckRandomHole(thisHoleCopy);
			} else {
				GameManager.instance.SetOldHole(thisHole);
			}
		}
		return thisHole;
	}

	public void SetHole(int resticionMin, int resticionMax, int randomCoin){
		System.Random rand = new System.Random();
		int hole = rand.Next(resticionMin,resticionMax);
		int catInThisPos = rand.Next(1,2);
		int catWasPut = 0;
		CheckSecondsWalls();

		for (int i = 0 ; i< 5 ; i++){
			if(floorTiles[i].transform.childCount > 0){
				coinGenerator.QuitACat(floorTiles[i].transform.GetChild(0).gameObject);
			}
			if(i == hole){
				floorTiles[i].tag = "Breakeable";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = breakeable;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
			}else if(i>=limite.min && i<limite.max){
				floorTiles[i].tag = "Floor";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = floor;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
				if (randomCoin > 2 && catWasPut == catInThisPos){
					coinGenerator.PutAcAT(floorTiles[i].gameObject);
					catWasPut++;
				}
				catWasPut++;
			}else{
				floorTiles[i].tag = "Floor";
				floorTiles[i].GetComponent<SpriteRenderer>().sprite = floor;
				floorTiles[i].GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
				if (CheckFloorPosition(floorTiles[i].gameObject)){
					this.transform.GetChild(9).gameObject.SetActive(true);
					this.transform.GetChild(9).transform.position = new Vector2(floorTiles[i].transform.position.x - 0.5f,this.transform.GetChild(9).transform.position.y);
				}else{
					this.transform.GetChild(8).gameObject.SetActive(true);
					this.transform.GetChild(8).transform.position = new Vector2(floorTiles[i].transform.position.x + 0.5f,this.transform.GetChild(8).transform.position.y);
				}
			}
		}
	}
}

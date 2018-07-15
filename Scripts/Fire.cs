using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fire : MonoBehaviour {
	public float timeToGrow = 0f;
	private float timeSpawn;
	public float growFireTime;
	public float retroceso;
	private float _retroceso;
	private float _growFireTime;
	public float finalYPos = 6.16f;
	public float startYPos;


	private BlockController blocks;

	Tween move;
	// Use this for initialization
	void Start () {
		this.transform.position = new Vector2(this.transform.position.x, startYPos);
		timeSpawn = timeToGrow;
		_retroceso = retroceso;
		_growFireTime = growFireTime + 3f;
		blocks = GameObject.Find("Block").GetComponent<BlockController>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSpawn -= Time.deltaTime;
		if(timeSpawn <= 0f){
			timeSpawn = timeToGrow;
			move.Kill();
			move = this.transform.DOMoveY(finalYPos, _growFireTime);
		}
/*		if(timeSpawn <= 0f){
			timeSpawn = timeToGrow;
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y + growFire, this.transform.localScale.z);
		}*/
		if (blocks.ReturnMove()){
			if(this.transform.position.y <= startYPos){
				move.Kill();
				move = this.transform.DOMoveY(startYPos, _retroceso);
				
				//this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - retroceso, this.transform.localScale.z);
				if (blocks.ReturnMove()){
			 		blocks.ChangeMoved();
				}
			}
		}
	}
}

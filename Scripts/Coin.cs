using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	public Sprite spriteCatAfter;
	private SpriteRenderer spriteCat;
	private Vector2 restartPos;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		restartPos = GameObject.Find("Block").GetComponent<CoinPoolGenerator>().GetDefaultPos();
		this.transform.GetChild(1).gameObject.SetActive(false);
	}
	public void Init(){
/*		spriteCat = this.GetComponent<SpriteRenderer>();
		spriteCat.sprite = this.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite;*/
		if (!this.transform.GetChild(0).gameObject.activeSelf){
			this.transform.GetChild(0).gameObject.SetActive(true);
			this.transform.GetChild(1).gameObject.SetActive(false);
		}
		//this.transform.GetChild(0).GetComponent<Animator>().Play("Caja Gato");
	}
	public void SetFree(){
		this.transform.GetChild(0).gameObject.SetActive(false);
		this.transform.GetChild(1).gameObject.SetActive(true);
		GameManager.instance.AddCats();
		StartCoroutine(CatDissapear());
	}

	IEnumerator CatDissapear(){
		yield return new WaitForSeconds(1f);
		this.transform.gameObject.SetActive(false);
		this.transform.position = restartPos;
	}
}

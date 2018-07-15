using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	public bool moveDown;
	bool isRight;
	Tween move;
	[SerializeField]
	BlockController block;

	[SerializeField]
	Config configuracion;

	[System.Serializable]
	struct Config
	{
		public float velocidadSeg;
		public float castigoFalloSeg;
		public float rangoMovimientoMin;
		public float rangoMovimientoMax;
	}

	void Start()
	{
		this.transform.position = new Vector2 (configuracion.rangoMovimientoMin,this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !moveDown){
			moveDown = true;
			move.Pause();
			GetComponentInChildren<Animator>().SetTrigger("Hit");
			//ManageMovements();
		}
		if(this.transform.position.x <= configuracion.rangoMovimientoMin + 0.1 && !moveDown){
			move.Kill();
			move = transform.DOMoveX(configuracion.rangoMovimientoMax, (configuracion.velocidadSeg * (configuracion.rangoMovimientoMax-1 - configuracion.rangoMovimientoMin))/6f).SetEase(Ease.Linear);
			this.transform.localScale = new Vector2(-1,this.transform.localScale.y);
			isRight = true;
		}
		else if(this.transform.position.x >= configuracion.rangoMovimientoMax - 0.1 && !moveDown){
			move.Kill();
			move = transform.DOMoveX(configuracion.rangoMovimientoMin, (configuracion.velocidadSeg * (configuracion.rangoMovimientoMax-1 - configuracion.rangoMovimientoMin))/6f).SetEase(Ease.Linear);
			this.transform.localScale = new Vector2(1,this.transform.localScale.y);
			isRight = false;
		}

	}

	IEnumerator Stop(){
		yield return new WaitForSeconds(configuracion.castigoFalloSeg);
		moveDown = false;
		move.Play();
	}

	public void ManageMovements(){
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 2f);
		Debug.DrawRay(this.transform.position, Vector2.down,Color.red,2f);
		if(hit.collider != null && hit.collider.gameObject.CompareTag("Breakeable")){
			StartCoroutine(Stop());
			block.Move();
			SFX.instance.Play_hitGood();
			GameManager.instance.AddLevel();
		}else if(hit.collider != null && hit.collider.transform.childCount > 0){
			//moveDown = true;
			//move.Pause();
			//StartCoroutine(Stop());
			hit.collider.transform.GetChild(0).gameObject.GetComponent<Coin>().SetFree();
			moveDown = false;
			SFX.instance.Play_hitBad();
			move.Play();
		}else {
			moveDown = false;
			SFX.instance.Play_hitBad();
			move.Play();
			//moveDown = true;
			//move.Pause();
			//StartCoroutine(Stop());
		}
	}

	public void resetVar(){
		configuracion.rangoMovimientoMin = block.actualFloor.limite.min-1-2;
		configuracion.rangoMovimientoMax = block.actualFloor.limite.max-2;
		float direccion = isRight?configuracion.rangoMovimientoMax:configuracion.rangoMovimientoMin;
		moveDown = false;
		move = transform.DOMoveX(direccion, (configuracion.velocidadSeg * Mathf.Abs(this.transform.position.x - direccion))/6f).SetEase(Ease.Linear);
	
		if (block.ReturnMove()){
			block.ChangeMoved();
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Fire"){
			move.Pause();
			move.Kill();
            DOVirtual.DelayedCall(0.2f, UIManager.instance.EnterGameoverCanvas);
		}
	}
}

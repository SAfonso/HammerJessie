using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockController : MonoBehaviour {

	[SerializeField]
	float speed;

	[SerializeField] public RandomCoin rCoin;
	[SerializeField] public struct RandomCoin{
		[Range(0,3)]
		public int min;
		[Range(2,6)]
		public int max;
	}
	
	[SerializeField]
	Floor[] floorBlocks;

	public Floor actualFloor;

	[SerializeField]
	PlayerController player;

	private bool isMoved = false;

	[SerializeField]
	Modulo[] modulos;

	[SerializeField]
	int probabilidadModulo;
	[SerializeField]
	int faseDelModulo;
	int moduloActual;

	public Sprite floor;
	public Sprite breakeable;

	[System.Serializable]
	struct Modulo{
		public Floor.Limites[] pisos;
	}

	void Start()
	{
		moduloActual = 0;
		faseDelModulo = modulos[moduloActual].pisos.Length;
	}



	public void SetCoinRandom(int min,int max){
		rCoin.min = min;
		rCoin.max = max;
	}

	public void Move() {
		foreach (Floor floor in floorBlocks){
			if(floor.transform.position.y + 2.5f < 6.9f){
				if(floor.transform.position.y + 2.5f == 2)
					actualFloor = floor;
				floor.transform.DOMoveY(floor.transform.position.y + 2.5f, speed);
			}else{
				floor.transform.DOMoveY(floor.transform.position.y + 2.5f, speed).OnComplete(()=>ResetPos(floor));
			}
		}
		isMoved = true;
	}

	public void ResetPos(Floor floor){
		System.Random rand = new System.Random();
		int coin = rand.Next(1,5);
		
		if(faseDelModulo == modulos[moduloActual].pisos.Length && rand.Next(0,100) > probabilidadModulo){
			floor.SetLimite(0,5);
			floor.SetHole(coin);
		}else if(faseDelModulo == modulos[moduloActual].pisos.Length){
			moduloActual = rand.Next(0,modulos.Length);
			faseDelModulo = 0;
			floor.SetLimite(modulos[moduloActual].pisos[faseDelModulo].min,modulos[moduloActual].pisos[faseDelModulo].max);
			floor.SetHole(modulos[moduloActual].pisos[faseDelModulo+1].min,modulos[moduloActual].pisos[faseDelModulo+1].max, coin);
			faseDelModulo++;
		}else {
			floor.SetLimite(modulos[moduloActual].pisos[faseDelModulo].min,modulos[moduloActual].pisos[faseDelModulo].max);
			if(faseDelModulo < modulos[moduloActual].pisos.Length - 1){
				floor.SetHole(modulos[moduloActual].pisos[faseDelModulo+1].min,modulos[moduloActual].pisos[faseDelModulo+1].max, coin);
			}
			else{
				floor.SetHole(coin);
			}
			faseDelModulo++;
		}
		floor.transform.position = new Vector2(0, -8f);
		player.resetVar();
	}

	public bool ReturnMove(){
		return isMoved;
	}

	public void ChangeMoved(){
		isMoved = !isMoved;
	}
}

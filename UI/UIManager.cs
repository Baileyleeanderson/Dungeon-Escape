using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private static UIManager _instance;
	public static UIManager Instance{
		get {
			if(_instance == null){

			}
			return _instance;
		}
	}

	public Text playerGemCountText;
	public Image selectionImg;
	public Text gemCountText;
	public Image[] healthBars;

	public void OpenShop(int gemCount){
		playerGemCountText.text = " " + gemCount + "G";
	}

	public void UpdateShopSelection(int yPos){
		Debug.Log("int item" + yPos);
		selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
	}

	public void UpdateGemCount(int count){
		gemCountText.text = " " + count;
	}

	private void Awake(){
		_instance = this;
	}

	public void UpdateLives(int livesRemaining){
		for(int i = 0; i <= livesRemaining; i++){
			Debug.Log("Updatelives" + livesRemaining);
			if(i == livesRemaining){
				healthBars[i].enabled = false;
			}
		}
	}
}

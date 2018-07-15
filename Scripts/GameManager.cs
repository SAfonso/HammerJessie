using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
	private int counterCats;
	private int counterLevel;
    private int oldHole;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        SFX.instance.Play_TitleMusic();
    }

    private void OnLevelWasLoaded(int level){
        if(SceneManager.GetActiveScene().buildIndex == 0){
            SFX.instance.Stop_GameMusic();
            SFX.instance.Stop_GameoverMusic();
            SFX.instance.Stop_FireClose();
            SFX.instance.Play_TitleMusic();
        }else{
            SFX.instance.Stop_TitleMusic();
            SFX.instance.Stop_GameoverMusic();
            SFX.instance.Play_FireClose();
            SFX.instance.Play_GameMusic();
        }
        InitLevel();

    }

	// Use this for initialization
	void InitLevel () {
        ResetValues();
        if(SceneManager.GetActiveScene().buildIndex != 0){
            UIManager.instance.EnterIngameCanvas();
        }
        UIManager.instance.HideGameoverCanvas();

	}

    public void ResetValues(){
        instance.oldHole = 6;
        instance.counterCats = 0;
        instance.counterLevel = 0;
        UIManager.instance.SetLevel(instance.counterLevel);
        UIManager.instance.SetCats(instance.counterCats);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetOldHole(int hole){
        instance.oldHole = hole;
    }

    public int GetOldHole(){
        return instance.oldHole;
    }

	public void AddCats(){
		instance.counterCats++;
		UIManager.instance.SetCats(instance.counterCats);
        SFX.instance.Play_gato();

	}

	public int GetLevel(){
		return instance.counterLevel;
	}

	public void AddLevel(){
		instance.counterLevel++;
		UIManager.instance.SetLevel(instance.counterLevel);

	}

	public int GetCats(){
		return instance.counterCats;
	}

	public void SaveTopScore() {
        if (!PlayerPrefs.HasKey("topScore"))
        {
            PlayerPrefs.SetInt("topScore", instance.counterCats);
        }
        else {
            /*int topScore = PlayerPrefs.GetInt("topScore");
            if (UIManager.instance.maxScore > topScore)
            {
                PlayerPrefs.SetInt("topScore", UIManager.instance.maxScore);
                UIManager.instance.SetTopScore(topScore);
            }
            else {
                UIManager.instance.SetTopScore(topScore);
            }*/
        }
    }
}

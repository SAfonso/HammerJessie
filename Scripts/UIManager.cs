using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour {

    [Header("UI INGAME")]
    [SerializeField] private CanvasGroup IngameCanvas;
    [SerializeField] private Transform PanelIngame;
    [SerializeField] private Image FadeIn;
    [SerializeField] private Text currentLevel, CatsRescued;
    [SerializeField] private Transform pivot_top;
    [Range(0f, 3f)] [SerializeField] private float Ingame_EnterTime;
    [Range(0f, 3f)] [SerializeField] private float Ingame_ExitTime;

    [Header("UI GAMEOVER")]
    [SerializeField] private CanvasGroup GameoverCanvas_Deco;
    [SerializeField] private CanvasGroup GameoverCanvas_Interactive;
    [SerializeField] private Text Gameover_Level, Gamover_Cats, Gameover_Highscore;
    [SerializeField] private Transform Button_Home, Button_Store, Button_Restart;
    [SerializeField] private Transform pivot_right;
    [Range(0f, 3f)] [SerializeField] private float Gameover_EnterTime;
    [Range(0f, 3f)] [SerializeField] private float Gameover_ExitTime;


    private Sequence mySeq;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


	   /*void Start () {
        HideGameoverCanvas();
        EnterIngameCanvas();
    }
 void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ExitIngameCanvas();
            DOVirtual.DelayedCall(0.5f, EnterGameoverCanvas);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            DOVirtual.DelayedCall(0.2f, ExitGameoverCanvas);
        }
    }
    */


    public void EnterIngameCanvas() {
       IngameCanvas.alpha = 1f;
        mySeq = DOTween.Sequence();
        mySeq.Append(FadeIn.DOFade(0f, Ingame_EnterTime));
        mySeq.Insert(Ingame_EnterTime * 0.25f ,PanelIngame.DOMoveY(pivot_top.transform.position.y, Ingame_EnterTime).From() );
    }

    public void ExitIngameCanvas() {
        mySeq = DOTween.Sequence();
        mySeq.Append(IngameCanvas.DOFade(0f, Ingame_ExitTime));
    }

    public void EnterGameoverCanvas() {
        mySeq = DOTween.Sequence();

        SFX.instance.Stop_FireClose();
        SFX.instance.Stop_GameMusic();
        SFX.instance.Play_GameoverMusic();

        mySeq.Append(GameoverCanvas_Deco.DOFade(1f, Gameover_EnterTime));
        mySeq.Insert(0f,GameoverCanvas_Interactive.DOFade(1, Gameover_EnterTime));
        mySeq.Append(Button_Home.DOScale(Vector3.one, Gameover_EnterTime/2).SetEase(Ease.OutBounce));
        mySeq.Insert(0.9f,Button_Store.DOScale(Vector3.one, Gameover_EnterTime/2).SetEase(Ease.OutBounce));
        mySeq.Append(Button_Restart.DOMoveX( pivot_right.position.x, Gameover_EnterTime/2).From().SetEase(Ease.OutExpo));
        mySeq.AppendCallback(() => ToggleGameoverCanvas(true) );
    }

    public void ExitGameoverCanvas() {
        mySeq = DOTween.Sequence();
        mySeq.AppendCallback(() => ToggleGameoverCanvas(false) );
        mySeq.Append(Button_Restart.DOMoveX(pivot_right.position.x, Gameover_ExitTime / 2).SetEase(Ease.InExpo));
        mySeq.Insert(0.35f,Button_Home.DOScale(Vector3.zero, Gameover_ExitTime / 2).SetEase(Ease.InExpo));
        mySeq.Insert(0.65f, Button_Store.DOScale(Vector3.zero, Gameover_ExitTime / 2).SetEase(Ease.InExpo));
        mySeq.Append(GameoverCanvas_Deco.DOFade(0f, Gameover_ExitTime));
        mySeq.Insert(0.95f, GameoverCanvas_Interactive.DOFade(0f, Gameover_ExitTime));
    }

    public void SetLevel(int newLevel) {
        Gameover_Level.text = newLevel.ToString();
        currentLevel.text = newLevel.ToString();
        currentLevel.transform.DOPunchScale(Vector3.one/2, Ingame_ExitTime / 2, 1);
    }

    public void SetCats(int newCats) {
        Gamover_Cats.text = newCats.ToString();
        CatsRescued.text = newCats.ToString();
        CatsRescued.transform.DOPunchScale(Vector3.one/2, Ingame_EnterTime / 3, 1);
    }

    public void SetHighscore(int newHiscore) {
        Gameover_Highscore.text = newHiscore.ToString();
    }

    public void HideGameoverCanvas() {
        GameoverCanvas_Deco.alpha = 0f;
        GameoverCanvas_Interactive.alpha = 0f;
        Button_Home.DOScale(Vector3.zero, 0f);
        Button_Store.DOScale(Vector3.zero, 0f);
        ToggleGameoverCanvas(false);
    }

    public void ToggleGameoverCanvas(bool toggleMode) {
        GameoverCanvas_Interactive.interactable = toggleMode;
        GameoverCanvas_Interactive.blocksRaycasts = toggleMode;
    }

}

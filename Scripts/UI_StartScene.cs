using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_StartScene : MonoBehaviour {

    [SerializeField] private CanvasGroup CanvasDeco, CanvasInteractive;

    [Header("Controles Transicion")]
    [Range(0f, 5f)]  [SerializeField] private float EnterTime;
    [Range(0f, 5f)]  [SerializeField] private float ExitTime;

    private Sequence mySeq;

    //quitar después, será llamado por el GameManager
    void Start()  {
        HideAllCanvas();
            EnterTitleScreen();
    }

    public void EnterTitleScreen() {
        mySeq = DOTween.Sequence();
        mySeq.Append(CanvasDeco.DOFade(1f, EnterTime));
        mySeq.Insert(0f, CanvasInteractive.DOFade(1f, EnterTime));
        mySeq.AppendCallback(() => ToggleUIBUttons(CanvasInteractive, true));
    }
    public void ExitTitleScreen() {
        mySeq = DOTween.Sequence();
        mySeq.AppendCallback(() => ToggleUIBUttons(CanvasInteractive, false));
        mySeq.Append(CanvasDeco.DOFade(0f, ExitTime));
        mySeq.Insert(0f, CanvasInteractive.DOFade(0f, ExitTime));
    }

    public void ToggleUIBUttons(CanvasGroup myGroup, bool toogleMode) {
        myGroup.interactable = toogleMode;
        myGroup.blocksRaycasts = toogleMode;
    }

    public void HideAllCanvas()
    {
        CanvasDeco.alpha = 0f;
        CanvasInteractive.alpha = 0f;
        CanvasInteractive.interactable = false;
        CanvasInteractive.blocksRaycasts = false;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    [SerializeField] GameObject TalkPanel;//* 對話介面
    [SerializeField] Text TalkText;//* 對話文字
    Sequence Seq;
    private void Awake()
    {
        Seq = DOTween.Sequence();
    }
    private void OnEnable()
    {
        GameRunSO.NpcTalkEvent += ToTalk;
    }
    private void OnDisable()
    {
        GameRunSO.NpcTalkEvent -= ToTalk;
    }
    private void ToTalk(string what, float time)
    {
        if (TalkPanel != null)
        {
            TalkPanel.SetActive(true);
            if (what != null)
            {
                Seq.Append(TalkText.DOText(null, 1));//? 開始播放第二句以上的對話時，清除前一個對話的進度
                Seq.Append(TalkText.DOText(what, time));//? 在指定秒數內逐漸一字一字顯示對話內容
            }
            else
            {
                TalkPanel.SetActive(false);
                Seq.Append(TalkText.DOText(null, 1));
            }
        }
    }
}

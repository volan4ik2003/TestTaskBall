using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public RectTransform winPanel;
    public TextMeshProUGUI MainText, ButtonText;
    private Vector2 winOriginalPosition;
    private Vector2 winHiddenPosition;

    void Start()
    {
        UpdateCoins();
        winOriginalPosition = winPanel.anchoredPosition;
        winHiddenPosition = new Vector2(winOriginalPosition.x, -winPanel.rect.height * 1.2f);
        winPanel.anchoredPosition = winHiddenPosition;
    }

    public void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        sequence.Append(winPanel.DOAnchorPos(winOriginalPosition, 1f).SetEase(Ease.OutCubic));
    }

    public void UpdateCoins()
    { 
        coinsText.text = "Coins: " + EconomyManager.Instance.playerCoins.ToString();
    }
}

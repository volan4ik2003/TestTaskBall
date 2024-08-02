using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public RectTransform winPanel;
    public TextMeshProUGUI MainText, ButtonText;
    public RectTransform shopPanel;
    public RectTransform settingsPanel;
    public Button shopToggleButton;
    public Button settingsToggleButton;

    private Vector2 winOriginalPosition;
    private Vector2 winHiddenPosition;
    private Vector2 shopOriginalPosition;
    private Vector2 shopHiddenPosition;
    private Vector2 settingsOriginalPosition;
    private Vector2 settingsHiddenPosition;
    private bool isShopOpen = false;
    private bool isSettingsOpen = false;
    private bool isAnimating = false;

    void Start()
    {
        UpdateCoins();

        winOriginalPosition = winPanel.anchoredPosition;
        winHiddenPosition = new Vector2(winOriginalPosition.x, -winPanel.rect.height * 1.2f);
        winPanel.anchoredPosition = winHiddenPosition;

        shopOriginalPosition = shopPanel.anchoredPosition;
        shopHiddenPosition = new Vector2(shopOriginalPosition.x, -shopPanel.rect.height * 1.2f);
        shopPanel.anchoredPosition = shopHiddenPosition;

        settingsOriginalPosition = settingsPanel.anchoredPosition;
        settingsHiddenPosition = new Vector2(settingsOriginalPosition.x, -settingsPanel.rect.height * 1.2f);
        settingsPanel.anchoredPosition = settingsHiddenPosition;

        shopToggleButton.onClick.AddListener(ToggleShopPanel);
        settingsToggleButton.onClick.AddListener(ToggleSettingsPanel);
    }

    public void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        sequence.Append(winPanel.DOAnchorPos(winOriginalPosition, 1f).SetEase(Ease.OutCubic));
    }

    public void ShowShopPanel()
    {
        shopPanel.gameObject.SetActive(true);
        isAnimating = true;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(shopPanel.DOAnchorPos(shopOriginalPosition, 1f).SetEase(Ease.OutCubic))
                .OnComplete(() => isAnimating = false);
    }

    public void HideShopPanel()
    {
        isAnimating = true;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(shopPanel.DOAnchorPos(shopHiddenPosition, 1f).SetEase(Ease.InCubic))
                .OnComplete(() =>
                {
                    shopPanel.gameObject.SetActive(false);
                    isAnimating = false;
                });
    }

    private void ToggleShopPanel()
    {
        if (isAnimating) return;

        if (isShopOpen)
        {
            HideShopPanel();
        }
        else
        {
            if (isSettingsOpen) HideSettingsPanel();
            ShowShopPanel();
        }
        isShopOpen = !isShopOpen;
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(true);
        isAnimating = true;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsPanel.DOAnchorPos(settingsOriginalPosition, 1f).SetEase(Ease.OutCubic))
                .OnComplete(() => isAnimating = false);
    }

    public void HideSettingsPanel()
    {
        isAnimating = true;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsPanel.DOAnchorPos(settingsHiddenPosition, 1f).SetEase(Ease.InCubic))
                .OnComplete(() =>
                {
                    settingsPanel.gameObject.SetActive(false);
                    isAnimating = false;
                });
    }

    private void ToggleSettingsPanel()
    {
        if (isAnimating) return;

        if (isSettingsOpen)
        {
            HideSettingsPanel();
        }
        else
        {
            if (isShopOpen) HideShopPanel();
            ShowSettingsPanel();
        }
        isSettingsOpen = !isSettingsOpen;
    }

    public void UpdateCoins()
    {
        coinsText.text = "Coins: " + EconomyManager.Instance.playerCoins.ToString();
    }
}

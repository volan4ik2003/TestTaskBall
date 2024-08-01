using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsPainted;

    private Color targetColor = Color.red; // ÷вет, в который €чейка должна изменитьс€
    public float animationDuration = 0.5f; // ƒлительность анимации

    private Color originalColor;
    private Vector3 originalScale;
    private Renderer cellRenderer;

    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        originalColor = cellRenderer.material.color;
        originalScale = transform.localScale;
    }

    public void AnimateCell()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(originalScale * 1.2f, animationDuration / 2))
                .Join(cellRenderer.material.DOColor(targetColor, animationDuration / 2))
                .Append(transform.DOScale(originalScale, animationDuration / 2));
    }
}

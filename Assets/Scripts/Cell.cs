using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsPainted;
    public bool IsStartCell;

    private Color targetColor = Color.red;
    public float animationDuration = 0.5f;

    private Vector3 originalScale;
    private Renderer cellRenderer;

    void Start()
    {
        cellRenderer = GetComponent<Renderer>();
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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundColorChanger : MonoBehaviour
{
    private Camera mainCamera;
    private Color originalColor = new Color(0.7601f, 0.9150f, 0.4791f);
    private float duration = 2f;

    void Start()
    {
        mainCamera = Camera.main;
        originalColor = mainCamera.backgroundColor;
    }

    public void ChangeBackgroundColor(Color newColor)
    {
        mainCamera.DOColor(newColor, duration);
    }

    public void ResetBackgroundColor()
    {
        mainCamera.DOColor(originalColor, duration);
    }
}

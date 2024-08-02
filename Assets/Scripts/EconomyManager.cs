using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    public int playerCoins;

    [SerializeField] private UIController UIController;

    private void OnEnable()
    {
        playerCoins = PlayerPrefs.GetInt("Money", 0);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        PlayerPrefs.SetInt("Money", playerCoins);
        UIController.UpdateCoins();
    }
}

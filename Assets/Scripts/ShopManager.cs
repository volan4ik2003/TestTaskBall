using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Button[] skinButtons;
    public Color[] skinColors; 
    public int[] skinPrices;
    public TextMeshProUGUI[] buttonLabels;

    private const string SelectedSkinKey = "SelectedSkin";
    private const string SkinBoughtPrefix = "SkinBought_";

    void Start()
    {
        UpdateButtonStates();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            int index = i;
            skinButtons[i].onClick.AddListener(() => OnSkinButtonClicked(index));
        }
    }

    public void OnSkinButtonClicked(int index)
    {
        if (PlayerPrefs.GetInt(SkinBoughtPrefix + index, 0) == 1 || index == 0)
        {
            PlayerPrefs.SetInt(SelectedSkinKey, index);
            UpdateButtonStates();
        }
        else
        {
            int playerCoins = EconomyManager.Instance.playerCoins;

            if (playerCoins >= skinPrices[index])
            {
                EconomyManager.Instance.AddCoins(-skinPrices[index]);
                PlayerPrefs.SetInt(SkinBoughtPrefix + index, 1);
                PlayerPrefs.SetInt(SelectedSkinKey, index); 
                UpdateButtonStates();
            }
            else
            {
                Debug.Log("Not Enough Coins");
            }
        }

        FindObjectOfType<PlayerController>().ApplySelectedSkin();
    }

    void UpdateButtonStates()
    {
        int selectedSkin = PlayerPrefs.GetInt(SelectedSkinKey, 0);

        for (int i = 0; i < skinButtons.Length; i++)
        {
            Button button = skinButtons[i];
            TextMeshProUGUI buttonLabel = buttonLabels[i];
            bool isBought = PlayerPrefs.GetInt(SkinBoughtPrefix + i, 0) == 1;
            bool isSelected = i == selectedSkin;

            if (i == 0 || isBought)
            {
                button.interactable = true;
                buttonLabel.text = isSelected ? "Selected" : "Select";
            }
            else
            {
                buttonLabel.text = skinPrices[i].ToString();
            }
        }
    }

}

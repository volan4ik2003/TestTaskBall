using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public GameObject[] levelsPrefabs;
    private int currentLevelIndex;
    [SerializeField] private Button nextLevelButton;

    private void OnEnable()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Level", 0);
        Instantiate(levelsPrefabs[currentLevelIndex]);
        nextLevelButton.onClick.AddListener(LoadLevel);
    }

    private void OnDisable()
    {
        nextLevelButton.onClick.RemoveAllListeners();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(0);    
    }
}

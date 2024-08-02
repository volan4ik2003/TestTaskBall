using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDuration = 0.5f;
    public Vector2Int gridSize = new Vector2Int(5, 5);
    public float cellSize = 1f;
    public float jumpPower = 1f;
    public int numJumps = 1;

    private Vector2Int currentGridPosition;
    private Vector2Int previousGridPosition; 
    private Cell previousCell;
    private bool isJumping = false;
    private bool isRevert = true;

    public FrontTrigger FrontTrigger, BackTrigger, LeftTrigger, RightTrigger;
    UIController uiController;

    private Renderer playerRenderer;
    private ShopManager shopManager;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        shopManager = FindObjectOfType<ShopManager>();
        ApplySelectedSkin();
        uiController = FindObjectOfType<UIController>();
        currentGridPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / cellSize),
            Mathf.RoundToInt(transform.position.z / cellSize)
        );
    }

    void Update()
    {
        if (isJumping) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && FrontTrigger.moveAllowed)
        {
            MoveBall(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && BackTrigger.moveAllowed)
        {
            MoveBall(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && LeftTrigger.moveAllowed)
        {
            MoveBall(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && RightTrigger.moveAllowed)
        {
            MoveBall(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !isRevert)
        {
            UndoMove();
        }
    }

    public void ApplySelectedSkin()
    {
        int selectedSkinIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        Color selectedColor = shopManager.skinColors[selectedSkinIndex];
        playerRenderer.material.color = selectedColor;
    }
    void MoveBall(Vector2Int direction)
    {
        isRevert = false;
        previousGridPosition = currentGridPosition;
        currentGridPosition += direction;

        UpdateBallPosition();
    }

    void UpdateBallPosition()
    {
        Vector3 newPosition = new Vector3(currentGridPosition.x * cellSize, transform.position.y, currentGridPosition.y * cellSize);
        isJumping = true;
        transform.DOJump(newPosition, jumpPower, numJumps, moveDuration).OnComplete(() => isJumping = false);
    }

    void UndoMove()
    {
        isRevert = true;
        if (previousCell != null)
        {
            previousCell.IsPainted = false;
            previousCell.RevertAnimation();
        }

        Vector3 previousPosition = new Vector3(previousGridPosition.x * cellSize, transform.position.y, previousGridPosition.y * cellSize);
        currentGridPosition = previousGridPosition;
        isJumping = true;
        transform.DOJump(previousPosition, jumpPower, numJumps, moveDuration).OnComplete(() => isJumping = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            Cell cell = other.GetComponent<Cell>();
            if (cell)
            {
                previousCell = cell;
                cell.IsPainted = true;
                cell.AnimateCell();
                isJumping = false;
            }
            CheckWinOrLose();
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            EconomyManager.Instance.AddCoins(1);
            Destroy(other.gameObject);
        }
    }

    void CheckWinOrLose()
    {
        bool allCellsPainted = true;
        foreach (Cell platform in FindObjectOfType<Platform>().cells)
        {
            if (!platform.IsPainted)
            {
                allCellsPainted = false;
                break;
            }
        }

        bool noMovesAllowed = !RightTrigger.moveAllowed && !LeftTrigger.moveAllowed &&
                              !FrontTrigger.moveAllowed && !BackTrigger.moveAllowed;

        if (allCellsPainted)
        {
            BackroundColorChanger colorChanger = FindObjectOfType<BackroundColorChanger>();
            if (colorChanger != null)
            {
                colorChanger.ChangeBackgroundColor(new Color(0.3237f, 0.9150f, 0.3501f));
            }

            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);

            uiController.MainText.text = "YOU WIN!";
            uiController.ButtonText.text = "NextLevel";
            uiController.ShowWinPanel();
        }
        else if (!allCellsPainted && noMovesAllowed)
        {
            BackroundColorChanger colorChanger = FindObjectOfType<BackroundColorChanger>();
            if (colorChanger != null)
            {
                colorChanger.ChangeBackgroundColor(new Color(0.9137f, 0.4784f, 0.4863f));
            }

            uiController.MainText.text = "YOU LOSE!";
            uiController.ButtonText.text = "RestartLevel";
            uiController.ShowWinPanel();
        }
    }
}

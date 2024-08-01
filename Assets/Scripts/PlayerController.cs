using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDuration = 0.5f; // ������������ ������
    public Vector2Int gridSize = new Vector2Int(5, 5); // ������� �������� ���� � �������
    public float cellSize = 1f; // ������ ����� ������
    public float jumpPower = 1f; // ������ ������
    public int numJumps = 1; // ���������� ������� (������ 1)

    private Vector2Int currentGridPosition; // ������� ������� ���� �� ������� ����
    private bool isJumping = false; // ����, ����� ������������� ������� �������

    public FrontTrigger FrontTrigger, BackTrigger, LeftTrigger, RightTrigger;

    void Start()
    {
        // ���������� ������� ������� ���� �� ������� ���� ������ �� ��� ��������� �������
        currentGridPosition = new Vector2Int(
            Mathf.RoundToInt(transform.position.x / cellSize),
            Mathf.RoundToInt(transform.position.z / cellSize)
        );

        // ��������� ������� ����, ����� ��� ��������������� ������
        UpdateBallPosition();
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
    }

    void MoveBall(Vector2Int direction)
    {
        Vector2Int newPosition = currentGridPosition + direction;

        // �������� �� ����� �� ������� ����
        currentGridPosition = newPosition;
        UpdateBallPosition();
    }

    void UpdateBallPosition()
    {
        Vector3 newPosition = new Vector3(currentGridPosition.x * cellSize, transform.position.y, currentGridPosition.y * cellSize);

        isJumping = true;
        transform.DOJump(newPosition, jumpPower, numJumps, moveDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cell"))
        {
            Cell cell = other.GetComponent<Cell>();
            if (cell)
            {
                isJumping = false;
                cell.IsPainted = true;   
                cell.AnimateCell();
            }
        }
    }
}

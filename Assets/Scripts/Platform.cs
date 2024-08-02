using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject coinPrefab; // Префаб монетки
    public int numberOfCoins = 5;

    public Cell[] cells;

    private void Start()
    {
        cells = GetComponentsInChildren<Cell>();

        SpawnCoins();
    }

    private void SpawnCoins()
    {
        Cell[] availableCells = System.Array.FindAll(cells, cell => !cell.IsStartCell);

        int coinsToSpawn = Mathf.Min(numberOfCoins, availableCells.Length);

        for (int i = availableCells.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Cell temp = availableCells[i];
            availableCells[i] = availableCells[j];
            availableCells[j] = temp;
        }

        for (int i = 0; i < coinsToSpawn; i++)
        {
            Vector3 spawnPosition = availableCells[i].transform.position + Vector3.up * 1.5f;
            Quaternion spawnRotation = coinPrefab.transform.rotation;
            Instantiate(coinPrefab, spawnPosition, spawnRotation, this.transform);
        }
    }
}

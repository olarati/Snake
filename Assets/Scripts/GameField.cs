using UnityEngine;

public class GameField : MonoBehaviour
{
    public Transform FirstCellPoint;
    public Vector2 CellSize;
    public int CellsInRow = 10;

    private Vector2[,] _cellsPositions;

    public void FillCellsPositions()
    {
        _cellsPositions = new Vector2[CellsInRow, CellsInRow];

        _cellsPositions[0, 0] = FirstCellPoint.position;
        for (int i = 0; i < CellsInRow; i++)
        {
            for (int j = 0; j < CellsInRow; j++)
            {
                _cellsPositions[i, j] = (Vector2) FirstCellPoint.position + Vector2.right * i * CellSize.x + Vector2.up * j * CellSize.y;
            }
        }
    }


    public Vector2 GetCellPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x >= CellsInRow || y >= CellsInRow)
        {
            return Vector2.zero;
        }

        return _cellsPositions[x, y];
    }

}

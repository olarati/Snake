using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameStateChanger GameStateChanger;
    public GameField GameField;
    public Snake Snake;

    public GameFieldObject ApplePrefab;

    private GameFieldObject _apple;

    public void CreateApple()
    {
        _apple = Instantiate(ApplePrefab);
        SetNextApple();
    }

    public void SetNextApple()
    {
        if (!_apple)
        {
            return;
        }
        if (!CheckHasEmptyCells())
        {
            GameStateChanger.EndGame();
            return;
        }

        int emptyCellsCount = GetEmptyCellsCount();
        Vector2Int[] possibleCellsIds = new Vector2Int[emptyCellsCount];

        int counter = 0;
        for (int i = 0; i < GameField.CellsInRow; i++)
        {
            for (int j = 0; j < GameField.CellsInRow; j++)
            {
                if (GameField.GetCellIsEmpty(i, j))
                {
                    possibleCellsIds[counter] = new Vector2Int(i, j); 
                    counter++;
                }
            }
        }

        Vector2Int appleCellId = possibleCellsIds[Random.Range(0, possibleCellsIds.Length)];
        GameField.SetObjectCell(_apple, appleCellId);
    }

    public Vector2Int GetAppleCellId()
    {
        return _apple.GetCellId();
    }

    private bool CheckHasEmptyCells()
    {
        return GetEmptyCellsCount() > 0;
    }

    private int GetEmptyCellsCount()
    {
        int snakePartsLength = Snake.GetSnakePartsLength();
        int fieldCellsCount = GameField.CellsInRow * GameField.CellsInRow;
        return fieldCellsCount - snakePartsLength;
    }


}

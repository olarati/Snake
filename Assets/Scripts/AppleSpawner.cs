using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameStateChanger GameStateChanger;
    public GameField GameField;
    public Snake Snake;

    public GameFieldObject ApplePrefab;
    public int StepsBeforeSpawn = 0;

    private GameFieldObject _apple;
    private int _stepCounter = -1;

    public void CreateApple()
    {
        _apple = Instantiate(ApplePrefab);
        SetNextApple();
    }

    public void Restart()
    {
        _stepCounter = -1;
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

        _stepCounter++;
        if(_stepCounter < StepsBeforeSpawn)
        {
            HideApple();
            return;
        }
        ShowApple();

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

    public void HideApple()
    {
        SetActiveApple(false);
    }

    public void ShowApple()
    {
        _stepCounter = 0;
        SetActiveApple(true);
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

    private void SetActiveApple(bool value)
    {
        _apple.gameObject.SetActive(value);
    }
}

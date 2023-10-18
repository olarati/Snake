using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameField GameField;

    public GameObject SnakeHeadPrefab;
    public GameObject SnakeBodyPrefab;

    public Vector2Int SnakeStartCell = new Vector2Int(5, 5);

    // значени€ в €чейках 0 - пусто, 1 - голова, 2 - тело
    private int[,] _snakeCells;

    public void CreateSnake()
    {
        _snakeCells = new int[GameField.CellsInRow, GameField.CellsInRow];
        _snakeCells[SnakeStartCell.x, SnakeStartCell.y] = 1;
        _snakeCells[SnakeStartCell.x, SnakeStartCell.y - 1] = 2;
        DrawSnake();
    }


    private void DrawSnake()
    {
        for (int i = 0; i < GameField.CellsInRow; i++)
        {
            for (int j = 0; j < GameField.CellsInRow; j++)
            {
                if (_snakeCells[i, j] == 1)
                {

                }
                else if (_snakeCells[i, j] == 2)
                {

                }
                else
                {

                }
            }

        }
    }
}

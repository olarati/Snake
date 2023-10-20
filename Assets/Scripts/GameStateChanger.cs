using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameField GameField;
    public Snake Snake;

    private void Start()
    {
        FirstStartGame();
    }

    private void FirstStartGame()
    {
        GameField.FillCellsPositions();
        Snake.CreateSnake();
    }
}

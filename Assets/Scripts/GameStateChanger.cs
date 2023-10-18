using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameField GameField;

    private void Start()
    {
        FirstStartGame();
    }

    private void FirstStartGame()
    {
        GameField.FillCellsPositions();
    }
}

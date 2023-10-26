using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameField GameField;
    public Snake Snake;
    public AppleSpawner AppleSpawner;

    public void EndGame()
    {
        Snake.StopGame();
    }
    public void StartGame()
    {
        Snake.StartGame();
        AppleSpawner.CreateApple();
    }

    private void Start()
    {
        FirstStartGame();
    }

    private void FirstStartGame()
    {
        GameField.FillCellsPositions();
        StartGame();
    }

    
}

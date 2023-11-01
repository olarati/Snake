using UnityEngine;
using TMPro;

public class GameStateChanger : MonoBehaviour
{
    public GameField GameField;
    public Snake Snake;
    public AppleSpawner[] AppleSpawners;
    public Score Score;

    public GameObject GameScreen;
    public GameObject GameEndScreen;

    public TextMeshProUGUI GameEndScoreText;
    public TextMeshProUGUI BestScoreText;

    private bool _isGameStarted;

    public void EndGame()
    {
        if (!_isGameStarted)
        {
            return;
        }
        _isGameStarted = false;
        Snake.StopGame();
        RefreshScores();
        SwitchScreens(false);
    }

    public void StartGame()
    {
        _isGameStarted = true;
        Snake.StartGame();
        for (int i = 0; i < AppleSpawners.Length; i++)
        {
            AppleSpawners[i].CreateApple();
        }
        SwitchScreens(true);
    }

    public void RestartGame()
    {
        _isGameStarted = true;
        Snake.RestartGame();
        for (int i = 0; i < AppleSpawners.Length; i++)
        {
            AppleSpawners[i].Restart();
        }
        Score.Restart();
        SwitchScreens(true);
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

    private void SwitchScreens(bool isGame)
    {
        GameScreen.SetActive(isGame);
        GameEndScreen.SetActive(!isGame);
    }

    private void RefreshScores()
    {
        int score = Score.GetScore();
        int oldBestScore = Score.GetBestScore();
        bool isNewBestScore = CheckNewBestScore(score, oldBestScore);
        SetActiveGameEndScoreText(!isNewBestScore);
        if (isNewBestScore)
        {
            Score.SetBestScore(score);
            SetNewBestScoreText(score);
        }
        else
        {
            SetGameEndScoreText(score);
            SetOldBestScoreText(oldBestScore);
        }
    }

    private bool CheckNewBestScore(int score, int oldBestScore)
    {
        return score > oldBestScore;
    }

    private void SetGameEndScoreText(int value)
    {
        GameEndScoreText.text = $"Игра окончена!\nКоличество очков: {value}";
    }

    private void SetOldBestScoreText(int value)
    {
        BestScoreText.text = $"Лучший результат: {value}";
    }

    private void SetNewBestScoreText(int value)
    {
        BestScoreText.text = $"Новый рекорд: {value}!";
    }

    private void SetActiveGameEndScoreText(bool value)
    {
        GameEndScoreText.gameObject.SetActive(value);
    }

}

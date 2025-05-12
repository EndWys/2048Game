using Assets._Project.Scripts.ServiceLocatorSystem;
using System;

public interface IGameScore : IService
{
    int Score { get; }
    void AddScore(int amount);
    void ResetScore();

    event Action<int> OnScoreChange;
}

public class GameScore : IGameScore
{
    public int Score { get; private set; }

    public event Action<int> OnScoreChange;

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChange?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
    }
}

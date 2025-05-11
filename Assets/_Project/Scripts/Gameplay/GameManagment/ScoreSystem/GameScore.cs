using Assets._Project.Scripts.ServiceLocatorSystem;
using UnityEngine;

public interface IGameScore : IService
{
    int Score { get; }
    void AddScore(int amount);
    void ResetScore();
}

public class GameScore : IGameScore
{
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        Score += amount;
        Debug.Log($"Score increased by {amount}. Total: {Score}");
    }

    public void ResetScore()
    {
        Score = 0;
    }
}

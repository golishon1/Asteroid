using System;

public static class EventManager
{
    public static event Action OnEndGame;
    public static event Action OnStartGame;

    public static void EndGame()
    {
        OnEndGame?.Invoke();
    }

    public static void StartGame()
    {
        OnStartGame?.Invoke();
    }
}
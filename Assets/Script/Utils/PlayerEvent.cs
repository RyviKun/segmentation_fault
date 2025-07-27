using System;
using UnityEngine;

public class PlayerEvent
{
    public static event Action OnPlayerMoved;

    public static void PlayerMoved()
    {
        OnPlayerMoved?.Invoke();
    }
}

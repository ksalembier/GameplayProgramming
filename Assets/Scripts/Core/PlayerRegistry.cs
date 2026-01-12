using System.Collections.Generic;
using UnityEngine;

public static class PlayerRegistry
{
    private static readonly List<PlayerController> players = new();

    public static IReadOnlyList<PlayerController> Players => players;

    public static void Register(PlayerController player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public static void Unregister(PlayerController player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerLevel;

    public int playedLevel;

    public PlayerData(int _playerLevel, int _playedLevel)
    {
        playerLevel = _playerLevel;

        playedLevel = _playedLevel;
    }
}

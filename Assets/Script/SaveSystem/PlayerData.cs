using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerLevel;

    public int playedLevel;

    public string levelname;

    public PlayerData(int _playerLevel, int _playedLevel, string _levelname)
    {
        playerLevel = _playerLevel;

        playedLevel = _playedLevel;

        levelname = _levelname;
    }
}

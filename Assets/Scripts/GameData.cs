using UnityEngine;

public enum CharacterType
{
    Wizard,
    Knight
}

public static class GameData
{
    public static CharacterType SelectedCharacter = CharacterType.Wizard; // default fallback
}
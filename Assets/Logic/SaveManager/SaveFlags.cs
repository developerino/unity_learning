using System;

[System.Flags]
public enum SaveFlags
{
    None = 0,
    Position = 1 << 0,
    Rotation = 1 << 1,
    Scale = 1 << 2,
    Health = 1 << 3,
    Stamina = 1 << 4,
    Mood = 1 << 5,
    Inventory = 1 << 6,
    Children = 1 << 7
}
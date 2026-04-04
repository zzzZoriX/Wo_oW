using System.Collections.Generic;
using KillTypes;

public static class CoolPointsModifiers {
    public static readonly Dictionary<KillType, float> Modifiers = new Dictionary<KillType, float>() {
        { KillType.Falling, 1.3f},
        { KillType.Moving, 1.2f}
    };
}
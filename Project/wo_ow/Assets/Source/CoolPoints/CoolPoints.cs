using System.Collections.Generic;
using UnityEngine;
using KillTypes;

public class CoolPoints : MonoBehaviour {
    public float Points { get; private set; }


    public void Set(float points = 0f) {
        Points = points;
    }

    public void Add(CoolPoints other, KillType type)
        => Points += other.Points * CoolPointsModifiers.Modifiers[type];

    public void Add(CoolPoints other, List<KillType> types) {
        var addPoints = other.Points;

        foreach (var type in types)
            addPoints *= CoolPointsModifiers.Modifiers[type];

        Points += addPoints;
    }
}
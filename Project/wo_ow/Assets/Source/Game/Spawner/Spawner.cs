using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private SpawnerConfig _spawnerConfig;

    public List<Entity> Spawn(List<Enemies> enemiesList) {
        var entitiesList = new List<Entity>();
        
        foreach (var enemy in enemiesList) {
            entitiesList.Add(
                Instantiate(
                    GetPrefab(enemy),
                    GeneratePosition(),
                    transform.rotation
                ).GetComponent<Entity>()
            );
        }

        return entitiesList;
    }

    private Vector3 GeneratePosition()
    {
        return new Vector3(
            Random.Range(_minPosition.x, _maxPosition.x),
            Random.Range(_minPosition.y, _maxPosition.y),
            Random.Range(_minPosition.z, _maxPosition.z)
        );
    }

    private GameObject GetPrefab(Enemies enemy) {
        switch (enemy) {
            case Enemies.NeonSoldier:
                return _spawnerConfig.NeonSoldierPrefab;
            case Enemies.EyeOfGod:
                return _spawnerConfig.EyeOfGodPrefab;
            case Enemies.Glitch:
                return _spawnerConfig.GlitchPrefab;
            
            default:
                return new GameObject();
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private Vector3 _minPosition;

    [SerializeField] private SpawnerInfo _spawnerInfo;
    
    public List<GameObject> Spawn(Enemies[] enemies) {
        var spawnedEnemies = new List<GameObject>();
        
        foreach(var _ in enemies) {
            var instantiatedEnemy = Instantiate(
                _spawnerInfo.NeonSoldierPrefab,
                GeneratePosition(),
                transform.rotation
            );

            spawnedEnemies.Append(instantiatedEnemy);
        }

        return spawnedEnemies;
    }

    private Vector3 GeneratePosition()
    {
        return new Vector3(
            Random.Range(_minPosition.x, _maxPosition.x),
            Random.Range(_minPosition.y, _maxPosition.y),
            Random.Range(_minPosition.z, _maxPosition.z)
        );
    }
}
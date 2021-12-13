using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    public Wave[] waves;
    public float preparationTime;
    public int reward;

    [System.Serializable]
    public struct Wave
    {
        [SerializeField] private EnemyWave[] enemyies;
        [SerializeField] private GameObject[] spawnPoints;
        [SerializeField] private float timeSpawn;

        public EnemyWave[] getEnemyies()
        {
            return enemyies;
        }

        public GameObject[] getSpawnPoints()
        {
            return spawnPoints;
        }

        public float getTimeSpawn()
        {
            return timeSpawn;
        }
    }

    [System.Serializable]
    public struct EnemyWave
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int count;

        public GameObject getPrefab()
        {
            return prefab;
        }

        public int getCount()
        {
            return count;
        }
    }
}
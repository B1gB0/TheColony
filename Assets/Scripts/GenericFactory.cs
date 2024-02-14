using UnityEngine;

public abstract class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Transform[] _spawnPoint;

    private float _spread = 5f;
    private int _numberSpawnPoint;

    private void Start()
    {
        while (_numberSpawnPoint < _spawnPoint.Length)
        {
            GetNewInstance();
        }
    }

    private T GetNewInstance()
    {
        Vector3 position = new Vector3(
        _spawnPoint[_numberSpawnPoint].transform.position.x + Random.Range(_spread, -_spread),
        _spawnPoint[_numberSpawnPoint].transform.position.y,
        _spawnPoint[_numberSpawnPoint].transform.position.z + Random.Range(_spread, -_spread));

        _numberSpawnPoint++;

        return Instantiate(_prefab, position, Quaternion.identity);
    }
}

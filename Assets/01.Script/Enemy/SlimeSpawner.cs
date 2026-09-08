using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]private float _spawnInterval = 2f;
    [SerializeField] private GameObject _slimePrefab;
    [SerializeField] private float _radius;
    private float _waitTime;
    private void Update()
    {
        if (_waitTime >= _spawnInterval)
        {
            _waitTime -= _spawnInterval;
            Spawn();
        }
        _waitTime += Time.deltaTime;
    }

    private void Spawn()
    {
        float angle = Random.Range(0, 360);
        angle=Mathf.Deg2Rad * angle;
        Instantiate(_slimePrefab, new Vector3(Mathf.Cos(angle)* _radius, Mathf.Sin(angle)* _radius, 0), Quaternion.identity);
        //Mathf.Sin(_radius)
    }
}

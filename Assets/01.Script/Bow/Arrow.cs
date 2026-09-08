using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField]private float _lifeTime = 5f;
    private float _waitTime;
    public int _damage;
    private void Awake()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        Vector2 movedir = mousePosition - (Vector2)transform.position;

        float radian = Mathf.Atan2(movedir.y, movedir.x);
        float degree = radian * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, degree);
        _rb.linearVelocity = movedir.normalized * _speed;

        _damage = UpgradeManager.Instance.UpgradeDict[typeof(ArrowDamageUpgrade)].CurrentValue;
        _speed = UpgradeManager.Instance.UpgradeDict[typeof(ArrowSpeedUpgrade)].CurrentValue;
    }
    private void Update()
    {
        if(_lifeTime>_waitTime)_waitTime += Time.deltaTime;
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Slime enemy))
        {
            enemy.OnDamage(_damage);
            Destroy(gameObject);
        }
    }
}

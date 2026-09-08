using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [field:SerializeField] public int Damage { get; private set; }
    [SerializeField] private int hp;
    private void Awake()
    {
        if (transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void Update()
    {
        transform.Translate(-1 * (transform.position.normalized) * Time.deltaTime * _speed);
    }
    public void OnDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GoldManager.Instance.AddGold(1);
    }
}

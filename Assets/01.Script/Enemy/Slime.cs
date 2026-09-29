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
        EffectManager.Instance.Play(EffectType.DamageText, transform.position, Damage);
        EffectManager.Instance.Play(EffectType.EnemyHit, transform.position);
        AudioManager.Instance.PlaySFX(AudioType.EnemyHit);
        if (hp <= 0)
        {
            EffectManager.Instance.Play(EffectType.EnemyDie, transform.position);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GoldManager.Instance.AddGold(1);
    }
}

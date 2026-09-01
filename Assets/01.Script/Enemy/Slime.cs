using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
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
    private void OnDestroy()
    {
        GoldManager.Instance.AddGold(1);
    }
}

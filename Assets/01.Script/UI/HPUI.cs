using TMPro;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private void Start()
    {
        HPManager.Instance.OnUpdateHealth += UpdateHealth;
        UpdateHealth(HPManager.Instance.HP);
    }

    private void UpdateHealth(int obj)
    {
        _text.text = "HP: " + HPManager.Instance.HP.ToString();
    }
}

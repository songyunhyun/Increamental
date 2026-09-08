using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private void Start()
    {
        GoldManager.Instance.OnUpdateGold += UpdateGold;
        UpdateGold(GoldManager.Instance.Gold);
    }

    private void UpdateGold(int obj)
    {
        _text.text = "Gold: " + GoldManager.Instance.Gold.ToString();
    }
}

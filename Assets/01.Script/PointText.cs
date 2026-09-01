using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PointText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    public void UpdateText()
    {
        _text.text=$"gold: {GoldManager.Instance.Gold}";
    }
}

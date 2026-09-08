using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    RectTransform _transform;
    public Vector3 originPosition;
    public Vector3 Position;
    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        HPManager.Instance.OnDeath += ShowUpgradePanel;
        
    }
    public void ShowUpgradePanel()
    {
        _transform.anchoredPosition = Position;
    }
    public void HideUpgradePanel()
    {
        _transform.anchoredPosition = originPosition;
    }
}

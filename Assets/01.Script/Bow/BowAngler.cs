using UnityEngine;
using UnityEngine.InputSystem;

public class BowAngler : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePosition= Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.rotation=Quaternion.Euler(0,0, Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg);
    }
}

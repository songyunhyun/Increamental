using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BowShooter : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab;
    public event Action OnArrowShot;
    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(_arrowPrefab, transform.position,Quaternion.identity);
            OnArrowShot?.Invoke();
        }
    }
}

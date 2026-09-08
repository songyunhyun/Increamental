using UnityEngine;

public class RestartBtn : MonoBehaviour
{
    public void RestartRun()
    {
        HPManager.Instance.ResetHP();
        Time.timeScale = 1.0f;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;

    private void Awake()
    {
        _clickButtonManager.Initialize();
    }
}

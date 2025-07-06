using UnityEngine;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _playerCharacter;
    [SerializeField] private TargetObjective _targetObjective;
    [SerializeField] private GameObject _winScreen;

    private bool itemCollected = false;

    private void OnEnable()
    {
        _targetObjective.OnTargetObjectiveCollected += SetBool;
    }

    private void OnDisable()
    {
        _targetObjective.OnTargetObjectiveCollected -= SetBool;
    }

    void Start()
    {
        _playerCharacter.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemCollected)
        {
            _winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SetBool()
    {
        itemCollected = true;
    }
}

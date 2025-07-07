using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetObjective : MonoBehaviour
{
    [SerializeField] private Image _objectiveLight;

    public event Action OnTargetObjectiveCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();

        OnTargetObjectiveCollected.Invoke();

        _objectiveLight.color = Color.red;
        rend.color = Color.white;
    }
}

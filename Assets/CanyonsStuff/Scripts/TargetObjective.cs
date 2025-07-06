using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TargetObjective : MonoBehaviour
{
    [SerializeField] private Image _objectiveLight;

    public event Action OnTargetObjectiveCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        OnTargetObjectiveCollected.Invoke();

        _objectiveLight.color = Color.red;
        rend.material.color = Color.white;
    }
}

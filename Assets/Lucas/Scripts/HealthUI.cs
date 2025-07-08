using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _image1;
    [SerializeField] private Image _image2;
    [SerializeField] private Image _image3;
    [SerializeField] private GameObject _gameoverUI;
    void Start()
    {
        FindFirstObjectByType<PlayerController>().damaged.AddListener(OnDamage);
    }

    private void OnDamage(int health)
    {
        if (health ==0)
        {
            _gameoverUI.SetActive(true);
            _image1.color = Color.clear;
        }
        else if (health == 1)
        {
            _image2.color = Color.clear;
        }
        else if (health == 2)
        {
            _image3.color = Color.clear;
        }
    }
}

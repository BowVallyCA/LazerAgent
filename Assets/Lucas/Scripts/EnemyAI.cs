using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private List<Transform> _movement = new List<Transform>();
    private Rigidbody2D _rigidbody;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private GameObject _droppedItem;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _movement.Add(_movement[0]);
        _movement.RemoveAt(0);
        _velocity = (transform.position - _movement[0].position).normalized;
        transform.LookAt(_movement[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.linearVelocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Path"))
        {
            _movement.Add(_movement[0]);
            _movement.RemoveAt(0);
            _velocity = (transform.position - _movement[0].position).normalized;
            transform.LookAt(_movement[0].position);
            return;
        }
        if(_droppedItem != null && collision.gameObject.GetComponent<ReflectingBullet>() != null)
        {
            GameObject clone = Instantiate(_droppedItem,transform.position,new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }
    }
}

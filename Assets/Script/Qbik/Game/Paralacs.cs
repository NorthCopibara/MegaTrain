using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralacs : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tiles;

    public int _speed;

    [SerializeField] private float _tagPosition;

    private List<Rigidbody2D> _rb = new List<Rigidbody2D>();
    private Vector2 _force;
    void Start()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            Rigidbody2D rb;
            rb = _tiles[i].GetComponent<Rigidbody2D>();
            _rb.Add(rb);
        }

        _force = new Vector2( -_speed, 0);

        Move();
    }

    void FixedUpdate()
    {
        Move();
        foreach (GameObject x in _tiles)
        {
            if (x.transform.localPosition.x < -_tagPosition * 2)
            {
                x.transform.localPosition = new Vector3(_tagPosition, x.transform.localPosition.y, x.transform.localPosition.z);
            }
        }
    }

    private void Move()
    {
        if (_rb != null)
        {
            foreach (Rigidbody2D x in _rb)
                x.velocity = new Vector2(-_speed, x.velocity.y);
        }
    }

    private void Stope()
    {
        if (_rb != null)
        {
            foreach (Rigidbody2D x in _rb)
                x.velocity = Vector2.zero;
        }
    }
}

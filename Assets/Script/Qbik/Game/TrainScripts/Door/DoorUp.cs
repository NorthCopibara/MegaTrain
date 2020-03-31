using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUp : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _train;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _button.SetActive(true);
            _button.GetComponent<EventData>().SetButton(collision.gameObject, _train, 3);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _button.SetActive(false);
        }
    }
}

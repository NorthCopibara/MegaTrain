using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<ICam>().SwapCam(0, 1); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<ICam>().SwapCam(1, 0);
        }
    }
}

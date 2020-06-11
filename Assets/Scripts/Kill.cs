using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Predator")
        {
            Debug.Log("Kill");
            gameObject.SetActive(false);
        }
    }
}
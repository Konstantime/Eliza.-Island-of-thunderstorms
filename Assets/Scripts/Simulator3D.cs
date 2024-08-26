using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Simulator3D : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z - 2f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + 2f);
        }
    }

}

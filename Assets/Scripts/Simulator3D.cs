using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Simulator3D : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = 6;
            // transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z - 2f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.sortingOrder = 4;
            // transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + 2f);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum StatusOfActionButton
{
    None,
    CanPickUpAnItem,
    CanDropAnItem,
}
public class Player : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Joystick joystick;

    private SpriteRenderer spriteRenderer;

    
    // // Добавьте эти поля для анимации
    private Animator animator;  // Ссылка на Animator

    private Resource SelectedResource = null;
    [SerializeField] private GameObject ButtonPickUpResource;
    [SerializeField] private float speed;
    private Vector2 moveInput;
    // public event EventHandler PlayerDroppedItemFromInventory;
    // public event EventHandler PlayerPickedUpItemToInventory;

    private void Start(){
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        moveInput = joystick.Direction;
        transform.Translate(moveInput * Time.deltaTime * speed);

        if( moveInput.x != 0 || moveInput.y != 0 ) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);

        if( moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Resource")
        {
            Resource resource = other.GetComponent<Resource>();
            ButtonPickUpResource.SetActive(true);
            if (SelectedResource != resource)
            {
                SelectedResource = resource;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Resource")
        {
            ButtonPickUpResource.SetActive(false);
            Resource resource = other.GetComponent<Resource>();

            SelectedResource = null;
        }
    }

    public void PickUpItem()
    {
        inventory.AddResource(SelectedResource.nameResource);
        SelectedResource.DestroyYourself();
        SelectedResource = null;
    }
    // public void PickedUpAnItemToInventory()
    // {
    //     PlayerPickedUpItemToInventory?.Invoke(this, EventArgs.Empty);
    // }
}
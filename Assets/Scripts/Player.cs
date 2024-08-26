using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

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
    [SerializeField] private float speed;
    private Vector2 moveInput;
    private GameObject player;
    private List<Resource> objectsInContact = new List<Resource>();
    private ResourceType resourceTypeOfBeingTouched = ResourceType.None;

    public event EventHandler PlayerDroppedItemFromInventory;
    public event EventHandler PlayerPickedUpItemToInventory;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        moveInput = joystick.Direction;
        transform.Translate(moveInput * Time.deltaTime * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if( other.tag == "Resource" )
        {
            Resource resource = other.GetComponent<Resource>();
            resourceTypeOfBeingTouched = resource.nameResource;
            objectsInContact.Add(resource);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if( other.tag == "Resource" )
        {
            Resource resource = other.GetComponent<Resource>();
            objectsInContact.Remove(resource);
        }
    }

    public void ProcessTheActionButtonClick()
    {
        if( inventory.isInventoryEmpty() && objectsInContact.Count > 0 )
        {
            resourceTypeOfBeingTouched = objectsInContact[0].nameResource;
            inventory.PickUpItem( resourceTypeOfBeingTouched );
            objectsInContact[0].DestroyYourself();
        }
        else if( inventory.isInventoryEmpty() && objectsInContact.Count == 0 )
        {
            return;
        }
        else if( inventory.isInventoryEmpty() == false )
        {
            inventory.DropItem();
        }
    }
    
    public void DropAnItemFromInventory()
    {
        PlayerDroppedItemFromInventory?.Invoke(this, EventArgs.Empty);
    }

    public void PickedUpAnItemToInventory()
    {
        PlayerPickedUpItemToInventory?.Invoke(this, EventArgs.Empty);
    }
}









// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using NUnit.Framework.Constraints;
// using Unity.VisualScripting;
// using UnityEngine;

// public enum StatusOfActionButton
// {
//     None,
//     CanPickUpAnItem,
//     CanDropAnItem,
// }
// public class Player : MonoBehaviour
// {
//     [SerializeField] private Inventory inventory;
//     [SerializeField] private Joystick joystick;
//     [SerializeField] private float speed;
//     private StatusOfActionButton statusOfActionButton;
//     private Vector2 moveInput;
//     private GameObject player;
//     private GameObject objectInContact;
//     private ResourceType resourceTypeOfBeingTouched = ResourceType.None;

//     public event EventHandler PlayerDroppedItemFromInventory;
//     public event EventHandler PlayerPickedUpItemToInventory;

//     private void Start()
//     {
//     }

//     private void FixedUpdate()
//     {
//         moveInput = joystick.Direction;
//         transform.Translate(moveInput * Time.deltaTime * speed);
//     }
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if( other.tag == "Resource" )
//         {
//             Resource resource = other.GetComponent<Resource>();
//             resourceTypeOfBeingTouched = resource.nameResource;
//             if( statusOfActionButton == StatusOfActionButton.None )
//             {
//                 statusOfActionButton = StatusOfActionButton.CanPickUpAnItem;
//             }
//             objectInContact = other.GetComponent<GameObject>();
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if( other.tag == "Resource" )
//         {
//             if( statusOfActionButton == StatusOfActionButton.CanPickUpAnItem )
//             {
//                 statusOfActionButton = StatusOfActionButton.None;
//             }
//             objectInContact = null;
//         }
//     }

//     public void ProcessTheActionButtonClick()
//     {
//         Debug.Log("statusOfActionButton = " + statusOfActionButton);
//         Debug.Log("objectInContact == null " + (objectInContact == null));
//         // Debug.Log("objectInContact Name" + objectInContact.name);

//         if( inventory.isInventoryEmpty() && objectInContact != null )
//         {
//             поднимаем предмет
//         }
//         else if( inventory.isInventoryEmpty() && objectInContact == null )
//         {
//             return;
//         }
//         else if( inventory.isInventoryEmpty() == false )
//         {
//             выбрасываем предмет
//         }

//         // if( statusOfActionButton == StatusOfActionButton.None )
//         // {
//         //     return;
//         // }
//         // else if( statusOfActionButton == StatusOfActionButton.CanDropAnItem  ) // && objectInContact == null
//         // {
//         //     inventory.DropItem();
//         //     statusOfActionButton = StatusOfActionButton.None;
//         //     // DropAnItemFromInventory(  );
//         // }
//         // // else if( statusOfActionButton == StatusOfActionButton.CanDropAnItem && objectInContact != null )
//         // // {
//         // //     // говорим инвенторю поменять местами прикосаемый предмет и тот что в руках
//         // // }
//         // else if( statusOfActionButton == StatusOfActionButton.CanPickUpAnItem )
//         // {
//         //     inventory.PickUpItem( resourceTypeOfBeingTouched );
//         //     statusOfActionButton = StatusOfActionButton.CanDropAnItem;
//         //     Destroy(objectInContact);
//         //     resourceTypeOfBeingTouched = ResourceType.None;
//         //     // statusOfActionButton = StatusOfActionButton.None;
//         //     // PickedUpAnItemToInventory();
//         // }
//     }
//     public void DropAnItemFromInventory()
//     {
//         PlayerDroppedItemFromInventory?.Invoke(this, EventArgs.Empty);
//     }
//     public void PickedUpAnItemToInventory()
//     {
//         PlayerPickedUpItemToInventory?.Invoke(this, EventArgs.Empty);
//     }
// }
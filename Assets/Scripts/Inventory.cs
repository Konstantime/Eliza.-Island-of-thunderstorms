using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    // [SerializeField] Sprite[] spritesResources;
    // [SerializeField] Dictionary<ResourceType, GameObject> resources; // = new Dictionary<ResourceType, GameObject>()
    // [SerializeField] Sprite[] spritesBuildings;
    [SerializeField] GameObject[] resources;
    private GameObject item;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.PlayerDroppedItemFromInventory += Inventory_OnItemDropped;
        player.PlayerPickedUpItemToInventory += Inventory_OnItemPickedUpItem;
    }

    private void FixedUpdate()
    {
        if (item != null)
        {
            item.transform.position = transform.position;
        }
    }

    public void PlayerRaisedResource( Resources resources )
    {}

    private void Inventory_OnItemDropped(object sender, EventArgs e)
    {
        Debug.Log("Инвентарь знает о событии бросания предмета" + sender);
    }
    private void Inventory_OnItemPickedUpItem(object sender, EventArgs e)
    {
        Debug.Log("инвентарь поднял предмет");
    }
    public void DropItem()
    {
        item = null;
    }
    public void PickUpItem( ResourceType iteml )
    {
        switch (iteml)
        {
            case ResourceType.Stick:
                item = Instantiate(resources[0], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
                break;
            case ResourceType.Stone:
                item = Instantiate(resources[1], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
                break;
            case ResourceType.Obsidian:
                item = Instantiate(resources[2], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
                break;
            case ResourceType.Branch:
                item = Instantiate(resources[3], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
                break;
        }
    }
    public bool isInventoryEmpty()
    {
        return item == null;
    }
}



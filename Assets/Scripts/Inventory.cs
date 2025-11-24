using System;
using System.Collections.Generic;
using UnityEngine;

public enum ToolsType
{
    Knife,
    Axe,
    Hoe,
    Jug,
    FishingRod,
    CrabTrap
}

public enum FoodType {
    RawFish,
    CookedFish,
    CookedCrab
}

public enum ResourcesType {
    Stick,
    Stone,
    PalmLeaf,
    Algae,
    SharkTooth,
    Clay,
    WoodBoard
}


public class Inventory : MonoBehaviour
{
    private Dictionary<ToolsType, int> tools = new Dictionary<ToolsType, int>()
    {
        { ToolsType.Knife, 0},
        { ToolsType.Axe, 0},
        { ToolsType.Hoe, 0},
        { ToolsType.Jug, 0},
        { ToolsType.FishingRod, 0},
        { ToolsType.CrabTrap, 0}
    };

    private Dictionary<FoodType, int> food = new Dictionary<FoodType, int>()
    {
        { FoodType.RawFish, 0},
        { FoodType.CookedFish, 0},
        { FoodType.CookedCrab, 0},
    };

    private Dictionary<ResourcesType, int> resources = new Dictionary<ResourcesType, int>()
    {
        { ResourcesType.Stick , 0},
        { ResourcesType.Stone , 0},
        { ResourcesType.PalmLeaf , 0},
        { ResourcesType.Algae, 0},
        { ResourcesType.SharkTooth, 0},
        { ResourcesType.Clay, 0},
        { ResourcesType.WoodBoard, 0}
    };

    [SerializeField] private List<InventoryResourceButton> resourceButtons;
    public void AddResource(ResourcesType resource)
    {
        switch (resource)
        {
            case ResourcesType.Stone:
                resources[ResourcesType.Stone] += 1;
                break;
            case ResourcesType.Stick:
                resources[ResourcesType.Stick] += 1;
                break;
            case ResourcesType.PalmLeaf:
                resources[ResourcesType.PalmLeaf] += 1;
                break;
            case ResourcesType.Algae:
                resources[ResourcesType.Algae] += 1;
                break;
            case ResourcesType.SharkTooth:
                resources[ResourcesType.SharkTooth] += 1;
                break;
            case ResourcesType.Clay:
                resources[ResourcesType.Clay] += 1;
                break;
            case ResourcesType.WoodBoard:
                resources[ResourcesType.WoodBoard] += 1;
                break;
            default:
                Debug.LogWarning("Не правильный ресурс");
                break;
        }

        Debug.Log( resources[ResourcesType.Stick] );
        Debug.Log( resources[ResourcesType.Stone] );
    }

    public int GetCountResourcesByType(ResourcesType resourcesType) {
        Debug.Log( resourcesType + " " + resources[resourcesType]);
        return resources[resourcesType];
    }

    public void UpdateAllInventoryButton()
    {
        for (int i = 0; i < resourceButtons.Count; i++)
        {
            resourceButtons[i].UpdateCount();
        }
    }



    // [SerializeField] Sprite[] spritesResources;
    // [SerializeField] Dictionary<ResourceType, GameObject> resources; // = new Dictionary<ResourceType, GameObject>()
    // [SerializeField] Sprite[] spritesBuildings;


    // [SerializeField] SpriteRenderer spriteRenderer;
    // [SerializeField] GameObject[] resources;
    // private GameObject item;
    // private Player player;
    // private void Start()
    // {
    //     player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    //     player.PlayerDroppedItemFromInventory += Inventory_OnItemDropped;
    //     player.PlayerPickedUpItemToInventory += Inventory_OnItemPickedUpItem;
    // }

    // private void FixedUpdate()
    // {
    //     if (item != null)
    //     {
    //         item.transform.position = transform.position;
    //     }
    // }

    // public void PlayerRaisedResource( Resources resources )
    // {}

    // private void Inventory_OnItemDropped(object sender, EventArgs e)
    // {
    //     Debug.Log("Инвентарь знает о событии бросания предмета" + sender);
    // }
    // private void Inventory_OnItemPickedUpItem(object sender, EventArgs e)
    // {
    //     Debug.Log("инвентарь поднял предмет");
    // }
    // public void DropItem()
    // {
    //     item = null;
    // }
    // public void PickUpItem( ResourceType iteml )
    // {
    //     switch (iteml)
    //     {
    //         case ResourceType.Stick:
    //             item = Instantiate(resources[0], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
    //             break;
    //         case ResourceType.Stone:
    //             item = Instantiate(resources[1], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
    //             break;
    //         case ResourceType.Obsidian:
    //             item = Instantiate(resources[2], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
    //             break;
    //         case ResourceType.Branch:
    //             item = Instantiate(resources[3], new Vector3( transform.position.x, transform.position.y, transform.position.z ), Quaternion.identity );
    //             break;
    //     }
    // }
    // public bool isInventoryEmpty()
    // {
    //     return item == null;
    // }
}

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

public enum ToolsType
{
    Knife,
    Axe,
    Hoe,
    Jug,
    FishingRod
}

public enum FoodType {
    RawFish,
    CookedFish,
    Crab,
    Water
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
    private Dictionary<ToolsType, int> availableTools = new Dictionary<ToolsType, int>()
    {
        { ToolsType.Knife, 0},
        { ToolsType.Axe, 10},
        { ToolsType.Hoe, 0},
        { ToolsType.Jug, 0},
        { ToolsType.FishingRod, 0}
    };

    private Dictionary<FoodType, int> availableFood = new Dictionary<FoodType, int>()
    {
        { FoodType.RawFish, 0},
        { FoodType.CookedFish, 10},
        { FoodType.Crab, 0},
        { FoodType.Water, 0}
    };

    private Dictionary<ResourcesType, int> availableResources = new Dictionary<ResourcesType, int>()
    {
        { ResourcesType.Stick , 0},
        { ResourcesType.Stone , 0},
        { ResourcesType.PalmLeaf , 90},
        { ResourcesType.Algae, 20},
        { ResourcesType.SharkTooth, 0},
        { ResourcesType.Clay, 0},
        { ResourcesType.WoodBoard, 40}
    };

    [SerializeField] private List<InventoryResourceButton> resourceButtons;

    
    [SerializeField] private List<InventoryFoodButton> foodButtons;
    [SerializeField] private List<InventoryToolButton> toolButtons;
    public void AddResource(ResourcesType resource)
    {
        switch (resource)
        {
            case ResourcesType.Stone:
                availableResources[ResourcesType.Stone] += 1;
                break;
            case ResourcesType.Stick:
                availableResources[ResourcesType.Stick] += 1;
                break;
            case ResourcesType.PalmLeaf:
                availableResources[ResourcesType.PalmLeaf] += 1;
                break;
            case ResourcesType.Algae:
                availableResources[ResourcesType.Algae] += 1;
                break;
            case ResourcesType.SharkTooth:
                availableResources[ResourcesType.SharkTooth] += 1;
                break;
            case ResourcesType.Clay:
                availableResources[ResourcesType.Clay] += 1;
                break;
            case ResourcesType.WoodBoard:
                availableResources[ResourcesType.WoodBoard] += 1;
                break;
            default:
                Debug.LogWarning("Не правильный ресурс");
                break;
        }
    }

    private void Start()
    {
        Hut hut = FindObjectOfType<Hut>();
        if (hut != null)
        {
            hut.onNewDay.AddListener(HandleNewDay);
        }
    }
    private void HandleNewDay()
    {
        ThrowOffAllAvailableResourcesToolsFoods();
    }

    private void ThrowOffAllAvailableResourcesToolsFoods()
    {
        // foreach (var key in availableResources.Keys)
        // {
        //     availableResources[key] = 0;
        // }
        // foreach (var key in availableFood.Keys)
        // {
        //     availableFood[key] = 0;
        // }
        // foreach (var key in availableTools.Keys)
        // {
        //     availableTools[key] = 0;
        // }

        foreach (var key in availableResources.Keys.ToList())
        {
            availableResources[key] = 0;
        }
        foreach (var key in availableFood.Keys.ToList())
        {
            availableFood[key] = 0;
        }
        foreach (var key in availableTools.Keys.ToList())
        {
            availableTools[key] = 0;
        }


    }

    public int GetCountResourcesByType(ResourcesType resourcesType) {
        return availableResources[resourcesType];
    }

    public void UpdateAllInventoryResourceButton()
    {
        for (int i = 0; i < resourceButtons.Count; i++)
        {
            resourceButtons[i].UpdateCount();
        }
    }

    public int GetCountFoodByType( FoodType foodType)
    {
        return availableFood[foodType];
    }

    public void SpendResourceByType( ResourcesType resourcesType, int count){
        availableResources[resourcesType] -= count;
    }

    public void AddTool( ToolsType toolsType, int count){
        availableTools[toolsType] += count;

        UpdateFoodBottons();
        UpdateToolBottons();
        UpdateAllInventoryResourceButton();
    }

    public int GetCountToolByType( ToolsType toolType)
    {
        return availableTools[toolType];
    }

    public void UpdateFoodBottons()
    {
        for (int i = 0; i < foodButtons.Count; i++)
        {
            foodButtons[i].UpdateCount();
        }
    }
    
    public void UpdateToolBottons()
    {
        for (int i = 0; i < toolButtons.Count; i++)
        {
            toolButtons[i].UpdateTextCount();
        }
    }

    public void AddFood( FoodType food, int count )
    {
        availableFood[ food ] += count;
    }

    public void SpendCountFoodByType( FoodType foodType, int count )
    {
        availableFood[foodType] -= count;
    }
}
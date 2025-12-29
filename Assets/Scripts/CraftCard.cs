using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CraftCard : MonoBehaviour
{
    [SerializeField] private string Title;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private int CreationTimeInMinutes;
    [SerializeField] private TextMeshProUGUI textCreationTimeInMinutes;
    // [SerializeField] private GameObject Boat;
    
    [Header("Тип инструмента для крафта")]
    [SerializeField] private ToolsType toolTypeForCraft;
    
    private Inventory inventory;

    [Header("Требуемые инструменты")]
    [SerializeField] private int KnifeRequired = 0;
    [SerializeField] private int AxeRequired = 0;
    [SerializeField] private int HoeRequired = 0;
    [SerializeField] private int JugRequired = 0;

    [Header("Требуемые ингридиенты")]
    [SerializeField] private int StickRequired = 0;
    [SerializeField] private int StoneRequired = 0;
    [SerializeField] private int PalmLeafRequired = 0;
    [SerializeField] private int AlgaeRequired = 0;
    [SerializeField] private int SharkToothRequired = 0;
    [SerializeField] private int ClayRequired = 0;
    [SerializeField] private int WoodBoardRequired = 0;


    [Header("Служебные ссылки инструментов. НЕ ТРОГАТЬ")]
    [SerializeField] private GameObject CardKnife;
    [SerializeField] private GameObject CardAxe;
    [SerializeField] private GameObject CardHoe;
    [SerializeField] private GameObject CardJug;

    
    [SerializeField] private TextMeshProUGUI TextCardKnife;
    [SerializeField] private TextMeshProUGUI TextCardAxe;
    [SerializeField] private TextMeshProUGUI TextCardHoe;
    [SerializeField] private TextMeshProUGUI TextCardJug;



    [Header("Служебные ссылки Ресурсов. НЕ ТРОГАТЬ")]
    [SerializeField] private GameObject CardResource1;
    [SerializeField] private SpriteRenderer SpriteResource1;
    [SerializeField] private TextMeshProUGUI TextResource1;
    
    [SerializeField] private GameObject CardResource2;
    [SerializeField] private SpriteRenderer SpriteResource2;
    [SerializeField] private TextMeshProUGUI TextResource2;
    
    [SerializeField] private GameObject CardResource3;
    [SerializeField] private SpriteRenderer  SpriteResource3;
    [SerializeField] private TextMeshProUGUI TextResource3;
    
    [SerializeField] private GameObject CardResource4;
    [SerializeField] private SpriteRenderer SpriteResource4;
    [SerializeField] private TextMeshProUGUI TextResource4;

    
    private Dictionary<ResourcesType, int> resourcesRequired = new Dictionary<ResourcesType,int>();


    [Serializable] public struct ResourceIconEntry
    {
        public ResourcesType type;
        public Sprite icon;
    }
    [SerializeField] private ResourceIconEntry[] resourceIcon;
    private Dictionary<ResourcesType, Sprite> resourceIconsMap;
    private List<ResourcesType> resourcesTypes= new List<ResourcesType>();

    private void OnEnable()
    {
        Configure();
        UpdateAppearance();
        ShowRequiredResources();
        ShowRequiredTools();
    }

    private void ShowRequiredTools()
    {
        if( KnifeRequired != 0)
        {
            CardKnife.SetActive(true);
            TextCardKnife.text = Convert.ToString(KnifeRequired);
        }
        
        if( AxeRequired != 0)
        {
            CardAxe.SetActive(true);
            TextCardAxe.text = Convert.ToString(AxeRequired);
        }
        
        if( HoeRequired != 0)
        {
            CardHoe.SetActive(true);
            TextCardHoe.text = Convert.ToString(HoeRequired);
        }
        
        if( JugRequired != 0)
        {
            CardJug.SetActive(true);
            TextCardJug.text = Convert.ToString(JugRequired);
        }
    }

    private void ShowRequiredResources()
    {
        int indexCardResourse = 0;

        if( resourcesRequired.Count == 0 ) return;

        for( int i = 0; i < resourcesTypes.Count; i++)
        {
            if( resourcesRequired.ContainsKey( resourcesTypes[i] ) == true){
                if( indexCardResourse == 0){
                    CardResource1.SetActive(true);
                    SpriteResource1.sprite = resourceIconsMap[resourcesTypes[i]];
                    TextResource1.text = Convert.ToString(resourcesRequired[resourcesTypes[i]]);
                }
                else if( indexCardResourse == 1){
                    CardResource2.SetActive(true);
                    SpriteResource2.sprite = resourceIconsMap[resourcesTypes[i]];
                    TextResource2.text = Convert.ToString(resourcesRequired[resourcesTypes[i]]);
                }
                else if( indexCardResourse == 2){
                    CardResource3.SetActive(true);
                    SpriteResource3.sprite = resourceIconsMap[resourcesTypes[i]];
                    TextResource3.text = Convert.ToString(resourcesRequired[resourcesTypes[i]]);
                }
                else if( indexCardResourse == 3){
                    CardResource4.SetActive(true);
                    SpriteResource4.sprite = resourceIconsMap[resourcesTypes[i]];
                    TextResource4.text = Convert.ToString(resourcesRequired[resourcesTypes[i]]);
                }
                indexCardResourse ++;
            }
        }
    }

    private void UpdateAppearance()
    {
        textTitle.text = Title;
        textCreationTimeInMinutes.text = "Время: " + Convert.ToString(CreationTimeInMinutes) + " мин.";
    }

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        Configure();
        resourceIconsMap = new Dictionary<ResourcesType, Sprite>();
        foreach (var entry in resourceIcon)
        {
            if (!resourceIconsMap.ContainsKey(entry.type) && entry.icon != null)
                resourceIconsMap.Add(entry.type, entry.icon);
        }
    }

    private bool CheckTheEnoughResources(){
        int countResource = 0;

        for(int i = 0; i < resourcesTypes.Count; i++){
            countResource = inventory.GetCountResourcesByType(resourcesTypes[i]);

            if( countResource < resourcesRequired[resourcesTypes[i]]) {
                return false;
            }
        }
        return true;
    }

    private bool CheckTheEnoughTools(){
        if(inventory.GetCountToolByType(ToolsType.Knife) < KnifeRequired ) { return false; }
        if(inventory.GetCountToolByType(ToolsType.Axe) < AxeRequired ) { return false; }
        if(inventory.GetCountToolByType(ToolsType.Hoe) < HoeRequired ) { return false; }
        if(inventory.GetCountToolByType(ToolsType.Jug) < JugRequired ) { return false; }

        return true;
    }

    public void CreateTool(){
        if( CheckTheEnoughResources() == false ) return;

        if( CheckTheEnoughTools() == false ) return;

        inventory.AddTool(toolTypeForCraft, 1);

        for(int i = 0; i < resourcesTypes.Count; i++){
            inventory.SpendResourceByType( resourcesTypes[i], resourcesRequired[resourcesTypes[i]]);
        }

        inventory.UpdateAllInventoryResourceButton();
    }

    private void Configure()
    {
        resourcesRequired.Clear();
        resourcesTypes.Clear();

        if (StickRequired != 0) resourcesRequired[ResourcesType.Stick] = StickRequired;
        if (StoneRequired != 0) resourcesRequired[ResourcesType.Stone] = StoneRequired;
        if (PalmLeafRequired != 0) resourcesRequired[ResourcesType.PalmLeaf] = PalmLeafRequired;
        if (AlgaeRequired != 0) resourcesRequired[ResourcesType.Algae] = AlgaeRequired;
        if (SharkToothRequired != 0) resourcesRequired[ResourcesType.SharkTooth] = SharkToothRequired;
        if (ClayRequired != 0) resourcesRequired[ResourcesType.Clay] = ClayRequired;
        if (WoodBoardRequired != 0) resourcesRequired[ResourcesType.WoodBoard] = WoodBoardRequired;

        if (StickRequired != 0) AddResourceIfNotExists(ResourcesType.Stick);
        if (StoneRequired != 0) AddResourceIfNotExists(ResourcesType.Stone);
        if (PalmLeafRequired != 0) AddResourceIfNotExists(ResourcesType.PalmLeaf);
        if (AlgaeRequired != 0) AddResourceIfNotExists(ResourcesType.Algae);
        if (SharkToothRequired != 0) AddResourceIfNotExists(ResourcesType.SharkTooth);
        if (ClayRequired != 0) AddResourceIfNotExists(ResourcesType.Clay);
        if (WoodBoardRequired != 0) AddResourceIfNotExists(ResourcesType.WoodBoard);
    }

    private void AddResourceIfNotExists(ResourcesType resource)
    {
        if (!resourcesTypes.Contains(resource))
        {
            resourcesTypes.Add(resource);
        }
    }

    // public void Activate( GameObject boat )
    // {
    //     boat.SetActive(true);
    // }
}
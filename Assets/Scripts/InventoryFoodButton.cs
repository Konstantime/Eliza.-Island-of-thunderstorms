using System;
using UnityEngine;
using TMPro;

public class InventoryFoodButton : MonoBehaviour
{
    [SerializeField] private FoodType foodType;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text myText;

    [SerializeField] private int healthPoint;
    [SerializeField] private int hungerPoint;
    [SerializeField] private int thirstPoint;
    
    private int countItems = 0;

    public void UpdateCount()
    {
        countItems = inventory.GetCountFoodByType(foodType);
        myText.text = Convert.ToString(countItems);
    }

    public void ChangePlayerParameters()
    {
        countItems = inventory.GetCountFoodByType(foodType);
        if( countItems == 0 ) return;

        gameManager.IncreasePlayerHealth(healthPoint);

        if( hungerPoint > 0)
        {
            gameManager.IncreasePlayerHunger(hungerPoint);
        }
        if( thirstPoint > 0)
        {
            gameManager.IncreasePlayerThirst(thirstPoint);
        }

        inventory.SpendCountFoodByType( foodType, 1 );

        UpdateTextCount();
    }

    private void UpdateTextCount()
    {
        countItems = inventory.GetCountFoodByType(foodType);
        myText.text = Convert.ToString(countItems);
    }
}

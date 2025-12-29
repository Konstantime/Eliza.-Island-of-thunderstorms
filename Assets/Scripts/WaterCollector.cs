using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class WaterCollector : MonoBehaviour
{
    [SerializeField] private Animator animatorGear;
    [SerializeField] private Button ButtonPutJug;
    [SerializeField] private TextMeshProUGUI TextFeedBack;
    [SerializeField] private Button ButtonTakeBottle;
    [SerializeField] private GameObject ButtonOpenPanel;

    private int TotalSeconds = 200;
    private int CurrentLeftSeconds = 200;
    private Inventory inventory;
    private bool hasJug = false;
    private bool hasBottlle = false;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        UpdateButtonsStates();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonOpenPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonOpenPanel.SetActive(false);
        }
    }

    public void UpdateButtonsStates()
    {
        TextFeedBack.text = "";
        StopAnimation();
        if( hasJug == true){
            PlayAnimation();
            ButtonPutJug.interactable = false;
            ButtonTakeBottle.interactable = false;
        }
        else if( hasBottlle == true){
            StopAnimation();
            ButtonPutJug.interactable = false;
            ButtonTakeBottle.interactable = true;
        }
        else{
            StopAnimation();
            if( inventory.GetCountToolByType(ToolsType.Jug) > 0){
                ButtonPutJug.interactable = true;
            }
            else {
                ButtonPutJug.interactable = false;
                TextFeedBack.text = "У вас нет свободных кувшинов";
            }
            ButtonTakeBottle.interactable = false;
        }
    }

    private IEnumerator RepeatEverySecond()
    {
        while (true)
        {
            if( CurrentLeftSeconds <= 0)
            {
                CurrentLeftSeconds = TotalSeconds;
                hasJug = false;
                hasBottlle = true;
                break;
            }
            CurrentLeftSeconds --;
            TextFeedBack.text = "Оставшееся количество секунд: " + Convert.ToString(CurrentLeftSeconds);
            yield return new WaitForSeconds(1f);
        }
    }

    public void PutJug(){
        hasJug = true;
        StartCoroutine(RepeatEverySecond());
        UpdateButtonsStates();
    }

    public void TakeBottle(){
        inventory.AddFood(FoodType.Water, 2);
        hasBottlle = false;
        UpdateButtonsStates();
    }


    public void PlayAnimation()
    {
        animatorGear.SetBool("isPlaying", true);
        animatorGear.speed = 1f;
    }

    public void StopAnimation()
    {
        animatorGear.speed = 0f;
    }
}

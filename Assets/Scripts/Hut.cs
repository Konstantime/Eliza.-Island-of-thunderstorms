using UnityEngine;
using UnityEngine.Events;

public class Hut : MonoBehaviour
{
    [SerializeField] private GameObject buttonSpendNight;
    [System.Serializable] public class NewDayEvent : UnityEvent { }
    
    public NewDayEvent onNewDay;
    
    public void JumpIntoNewDay()
    {
        onNewDay?.Invoke();
        Debug.Log("Hut: Новый день!");
        
        // уничтожить все вещи
        // добавить один день
        // удалить все ресурсы с острова
        // заспавнить новые вещи на острове
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonSpendNight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonSpendNight.SetActive(false);
        }
    }
}
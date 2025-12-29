using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    [SerializeField] private Watch watch;

    [SerializeField] private IndicatorBar HealthBar;
    [SerializeField] private IndicatorBar HungerBar;
    [SerializeField] private IndicatorBar ThirstBar;

    private int playerHP = 100;
    private int playerHunger = 100;
    private int playerThirst = 100;
    private int currentDay = 0;

    [SerializeField] private int minutesPerHungerPoint = 20;
    [SerializeField] private int minutesPerThirstPoint = 15;

    private int minutesUntilHungerDecrease;
    private int minutesUntilThirstDecrease;

    private bool isReducingHealth = false;

    private void Start()
    {
        playerHP = 100;
        playerHunger = 100;
        playerThirst = 100;

        if( PlayerPrefs.HasKey("CurrentDay") ) currentDay = PlayerPrefs.GetInt("CurrentDay");
        else
        {
            PlayerPrefs.SetInt("CurrentDay", 1);
            currentDay = 1;
        }
        PlayerPrefs.Save();

        Hut hut = FindObjectOfType<Hut>();
        if (hut != null)
        {
            hut.onNewDay.AddListener(HandleNewDay);  // Подписываемся
        }

        minutesUntilHungerDecrease = minutesPerHungerPoint;
        minutesUntilThirstDecrease = minutesPerThirstPoint;
        watch.OnMinuteAdded.AddListener(OnMinuteAddedHandler);

        // ReduceHealth( 50 );
        // ReduceHunger( 80 );
    }
    
    public void HandleNewDay()
    {
        currentDay += 1;
        PlayerPrefs.SetInt( "CurrentDay", currentDay );
        PlayerPrefs.Save();
    }
    public int GetCurrentday(){
        return currentDay;
    }
    private void ActiveLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnMinuteAddedHandler()
    {
        CheckVitalityDecay();
        Debug.Log( playerHP );
        Debug.Log("Минута добавлена! Пиши логику жажды здесь");
    }

    private void CheckVitalityDecay()
    {
        minutesUntilHungerDecrease -= 1;
        minutesUntilThirstDecrease -= 1;

        if (minutesUntilHungerDecrease <= 0)
        {
            minutesUntilHungerDecrease = minutesPerHungerPoint;
            ReduceHunger( 1 );
        }
        
        if (minutesUntilThirstDecrease <= 0)
        {
            minutesUntilThirstDecrease = minutesPerThirstPoint;
            ReduceThirst( 1 );
        }

        if ((playerHunger == 0 || playerThirst == 0) && !isReducingHealth)
        {
            isReducingHealth = true;
            StartCoroutine(ReduceHealthDueToHungerOrThirst());
        }

        if( playerHP <= 0)
        {
            ActiveLosePanel();
        }
    }

    private void ReduceHunger( int count )
    {
        playerHunger -= count;
        if( playerHunger < 0)
        {
            playerHunger = 0;
        }
        HungerBar.ReduceValueParameter( count );
    }
    
    private void ReduceThirst( int count )
    {
        playerThirst -= count;
        if( playerThirst < 0)
        {
            playerThirst = 0;
        }
        ThirstBar.ReduceValueParameter( count );
    }

    private void ReduceHealth( int count)
    {
        playerHP -= count;
        if( playerHP < 0)
        {
            playerHP = 0;
        }
        // HealthBar.ReduceValueParameter( count );
        HealthBar.SetValueParameter( playerHP );
    }

    private IEnumerator ReduceHealthDueToHungerOrThirst()
    {
        isReducingHealth = true;
        while (true)
        {
            if( playerHunger == 0 || playerThirst == 0)
            {
                ReduceHealth( 1 );
            }
            else
            {
                isReducingHealth = false;
                break;
            }
            yield return new WaitForSeconds(1f);  // Ждём 1 секунду
        }
        isReducingHealth = false;
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);  // Перезагружает текущую сцену
    }

    private void OnDestroy()
    {
        // Отписка при уничтожении
        if (watch != null)
            watch.OnMinuteAdded.RemoveListener(OnMinuteAddedHandler);
    }

    public void IncreasePlayerHealth( int count)
    {
        playerHP += count;
        if(playerHP >= 100) playerHP = 100;

        HealthBar.IncreaseValueParameter( count );
    }

    public void IncreasePlayerHunger( int count)
    {
        playerHunger += count;
        if( playerHunger >= 100) playerHunger = 100;

        HungerBar.IncreaseValueParameter( count );
    }

    public void IncreasePlayerThirst( int count)
    {
        playerThirst += count;
        if( playerThirst > 100) playerThirst = 100;

        ThirstBar.IncreaseValueParameter(count);
    }
}
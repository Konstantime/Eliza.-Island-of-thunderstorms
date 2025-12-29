using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Watch : MonoBehaviour
{
    [SerializeField] private (int hours, int minutes) time = (9,0);
    [SerializeField] private GameManager gameManager;
    private (int hours, int minutes) modifiedTimeForPreviewMode = (0, 0);
    [SerializeField] private Text text;
    private bool isWatchInPreviewMode = false;
    private int currentDay = 0;
    
    public UnityEvent OnMinuteAdded;


    private void Start()
    {
        StartCoroutine(IncreaseTimeByMinute( 1 ));
        currentDay = gameManager.GetCurrentday();
        UpdateWatch();
    }

    private IEnumerator IncreaseTimeByMinute( int PeriodInSecondsOfRealTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(PeriodInSecondsOfRealTime);
            AddMinutes( 1 );
        }
    }

    public void AddMinutes(int minutes)
    {
        time = ReturnTimeWithAdditionMinutes(time, minutes);
        modifiedTimeForPreviewMode = ReturnTimeWithAdditionMinutes(modifiedTimeForPreviewMode, minutes);
        
        OnMinuteAdded?.Invoke();
        
        UpdateWatch();
    }

    private void UpdateWatch()
    {
        string output;
        if (isWatchInPreviewMode)
        {
            output = ReturnTimeConvertedInTheCorrectFormat( time ) + "\n=> " + ReturnTimeConvertedInTheCorrectFormat( modifiedTimeForPreviewMode );
        }
        else
        {
            output = ReturnTimeConvertedInTheCorrectFormat( time);
            modifiedTimeForPreviewMode = time;
        }
        text.text = "Day: " + Convert.ToString(currentDay) + "\n" + output;
    }
    public void UpdateModifiedTimeForPreviewMode( int addedMinutes )
    {
        isWatchInPreviewMode = true;
        modifiedTimeForPreviewMode = ReturnTimeWithAdditionMinutes(modifiedTimeForPreviewMode, addedMinutes);
        UpdateWatch();
    }
    public void SetModifiedTime()
    {
        isWatchInPreviewMode = false;
        text.text = "Day: " + Convert.ToString(currentDay) + "\n" + ReturnTimeConvertedInTheCorrectFormat( modifiedTimeForPreviewMode );
        time = modifiedTimeForPreviewMode;
    }
    private (int, int) ReturnTimeWithAdditionMinutes( (int hours, int minutes) startTime, int minutes )
    {
        (int hours, int minutes) modifiedTime = ( startTime.hours, startTime.minutes);
        int deltaHours = minutes / 60;
        int deltaMinutes = minutes - (deltaHours * 60);
        
        modifiedTime.minutes += deltaMinutes;
        if( modifiedTime.minutes >= 60 )
        {
            modifiedTime.minutes = modifiedTime.minutes - 60;
            deltaHours += 1;
        }

        modifiedTime.hours += deltaHours;
        if( modifiedTime.hours >= 24 )
        {
            modifiedTime.hours = modifiedTime.hours - 24;
        }
        return modifiedTime;
    }
    private string ReturnTimeConvertedInTheCorrectFormat( (int hours, int minutes) timeInUsualFormat )
    {
        return $"{timeInUsualFormat.hours:D1}:{timeInUsualFormat.minutes:D2}";
    }
}

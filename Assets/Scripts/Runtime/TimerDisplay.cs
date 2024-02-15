using System;
using Michsky.MUIP;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private ProgressBar _display;
    [SerializeField] private float _countDownTime;
    private float _timer;

    public event Action OnTimerEnd;

    public void Activate()
    {
        gameObject.SetActive(true);
        _display.minValue = 0;
        _display.maxValue = _countDownTime;
        _display.valueLimit = _countDownTime;
        _display.ChangeValue(_countDownTime);
        _timer = _countDownTime;
    }

    private void Update ()
    {
        _timer -= Time.deltaTime;
        _display.ChangeValue(_timer);
        

        if(_timer <= 0)
        {
            OnTimerEnd?.Invoke();
        }
    }
}
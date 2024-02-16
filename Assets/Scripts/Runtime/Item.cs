using System;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private Notification _notification = new("Item Picked Up", string.Empty);

    public UnityEvent OnPickup;

    public static event Action<float> OnShotSpeedChange;
    public static event Action<int> OnShotDamageChange;
    public static event Action<float> OnMoveSpeedChange;

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPickup?.Invoke();
            NotificationManager.Instance.DisplayNotification(_notification);
        }
    }

    public void ShotSpeed (float speedChange) => OnShotSpeedChange?.Invoke(speedChange);
    public void ShotDamage (int damageChange) => OnShotDamageChange?.Invoke(damageChange);
    public void MoveSpeed (float speedChange) => OnMoveSpeedChange?.Invoke(speedChange);
}

[Serializable]
public struct Notification
{
    public string Title;
    public string Description;

    public Notification(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
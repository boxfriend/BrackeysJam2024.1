using Boxfriend.Utils;
using UnityEngine;
using NotifManager = Michsky.MUIP.NotificationManager;

public class NotificationManager : SingletonBehaviour<NotificationManager>
{
    [SerializeField] private NotifManager _notificationPrefab;

    private ObjectPool<NotifManager> _notificationPool;

    private void Awake () => _notificationPool = new(Create, InPool, InPool, defaultSize: 5, maxSize: 10);

    private NotifManager Create ()
    {
        var obj = Instantiate(_notificationPrefab, transform);
        obj.Close();
        obj.onClose.AddListener(() => _notificationPool.ToPool(obj));
        return obj;
    }

    private void InPool (NotifManager manager) { }

    public void DisplayNotification(Notification info)
    {
        var notif = _notificationPool.FromPool();
        notif.transform.SetSiblingIndex(0);
        notif.title = info.Title;
        notif.description = info.Description;
        notif.UpdateUI();
        notif.Open();

    }
}

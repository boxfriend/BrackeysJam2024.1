using Boxfriend.Utils;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioResource _soundResource;
    [SerializeField] private Transform _soundPosition;

    public void PlaySound () => AudioManager.Instance.Play(_soundResource, _soundPosition.position);

    private void Reset () => _soundPosition = transform;
}
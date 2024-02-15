using Cinemachine;
using UnityEngine;

public class CinemachineInputEnabler : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider _input;

    public void OnPause (bool isPause)
    {
        if (isPause)
            _input.XYAxis.action.Disable();
        else
            _input.XYAxis.action.Enable();
    }
}
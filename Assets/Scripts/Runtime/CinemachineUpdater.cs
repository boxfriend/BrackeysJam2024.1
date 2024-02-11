using Cinemachine;
using UnityEngine;

public class CinemachineUpdater : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _brain;
    private void Awake () => _brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.ManualUpdate;
    public void Update () => _brain.ManualUpdate();

    private void Reset ()
    {
        _brain = GetComponent<CinemachineBrain>();
        _brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.ManualUpdate;
    }
}
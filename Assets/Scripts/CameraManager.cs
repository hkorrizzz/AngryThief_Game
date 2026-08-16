using UnityEngine;

using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _idleCamera;
    [SerializeField] private CinemachineCamera _followCamera;

    private void Awake()
    {
        SwitchIdleCam();
    }

    public void SwitchIdleCam()
    {
        _idleCamera.enabled = true;
        _followCamera.enabled = false;
    }
     public void SwitchFollowCam(Transform followTrans)
    {
        _followCamera.Follow = followTrans;

        _idleCamera.enabled = false;
        _followCamera.enabled = true;
    }
}

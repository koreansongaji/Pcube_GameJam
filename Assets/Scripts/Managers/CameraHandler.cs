using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _endCamera;
    void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.GameTime >= GameManager.Instance.GetEndTime() * 0.95f)
        {
            _mainCamera.gameObject.SetActive(false);
            _endCamera.gameObject.SetActive(true);
        }
    }
}

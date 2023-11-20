using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f; // Adjust the zoom speed as needed
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }
    /// <summary>
    /// zoom in camera to player when player die
    /// </summary>
    void Update()
    {
        if (virtualCamera != null && GameManager.Instance.player.isPlayerDead)
        {
            float currentFOV = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 3, Time.deltaTime * zoomSpeed);
            virtualCamera.m_Lens.OrthographicSize = currentFOV;
        }
    }

}


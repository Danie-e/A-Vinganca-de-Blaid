using Cinemachine;
using UnityEngine;

public class DynamicZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomInFOV = 30f;
    public float zoomOutFOV = 60f;
    public float zoomSpeed = 10f;

    void Update()
    {
        if (/* sua condi��o aqui */ true)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomInFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomOutFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
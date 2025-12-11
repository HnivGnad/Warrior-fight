using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;
    private float imageWidthOffset = 10f;

    [SerializeField] private ParallaxLayer[] backgroundLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitializeLayer();
    }
    private void FixedUpdate()
    {
        float currentCamaraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCamaraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCamaraPositionX;

        float cameraLeftEdge = (currentCamaraPositionX - cameraHalfWidth) + imageWidthOffset;
        float cameraRightEdge = (currentCamaraPositionX + cameraHalfWidth) - imageWidthOffset;

        foreach (ParallaxLayer layer in backgroundLayer)
        {
            layer.Move(distanceToMove);
            layer.LoopBackGround(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayer()
    {
        foreach (ParallaxLayer layer in backgroundLayer)
        {
            layer.CalculateImageWith();
        }
    }
}

using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;

    [SerializeField] private ParallaxLayer[] backgroundLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        float currentCamaraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCamaraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCamaraPositionX;

        foreach (ParallaxLayer layer in backgroundLayer)
        {
            layer.Move(distanceToMove);
        }
    }
}

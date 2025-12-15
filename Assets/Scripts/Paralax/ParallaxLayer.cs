using UnityEngine;
[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMutiplier;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWith()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }
    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMutiplier);
    }
    public void LoopBackGround(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageLeftEdge = background.position.x - imageHalfWidth;
        float imageRightEdge = background.position.x + imageHalfWidth;

        if(imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if(imageLeftEdge > cameraRightEdge)
        {
            background.position += Vector3.right * -imageFullWidth;
        }
    }
}

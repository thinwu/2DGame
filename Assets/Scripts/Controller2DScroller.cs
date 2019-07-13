using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/playlist?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz
[RequireComponent(typeof(BoxCollider2D))]
public class Controller2DScroller : MonoBehaviour
{
    public LayerMask collisionMask;
    public CollisionInfo collisions;
    const float skinWidth = .015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    BoxCollider2D Collider;
    RaycastOrigins raycastOrigins;

    float horizontalRaySpacing;
    float verticalRaySpacing;
    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();
    }
    public delegate void OnHitCallBack(RaycastHit2D hit);

    private void VerticalCollision(ref Vector3 velocity, OnHitCallBack onHitCall)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i< verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisions.below = (directionY == -1);
                collisions.above = (directionY == 1);
                onHitCall(hit);
            }
        }
    }

    private void HorizontalCollision(ref Vector3 velocity, OnHitCallBack onHitCall)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * horizontalRaySpacing * i;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
            if (hit)
            {
                
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisions.left = (directionX == -1);
                collisions.right = (directionX == 1);
                onHitCall(hit);
            }
        }
    }
    public void Move(Vector3 velocity, OnHitCallBack onHitCall)
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();
        collisions.Reset();
        if (velocity.x != 0)
        {
           HorizontalCollision(ref velocity, onHitCall);
        }
        if(velocity.y != 0)
        {
           VerticalCollision(ref velocity, onHitCall);
        }
        transform.Translate(velocity);
    }
    void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}

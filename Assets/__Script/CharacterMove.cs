using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 2f; // T?c ?? di chuy?n
    private bool movingRight = true;

    private float leftEdge;
    private float rightEdge;

    void Start()
    {
        // L?y mép trái/ph?i màn hình theo camera chính
        Camera cam = Camera.main;
        float z = transform.position.z; // ?? sâu hi?n t?i c?a nhân v?t

        leftEdge = cam.ScreenToWorldPoint(new Vector3(0, 0, z)).x;
        rightEdge = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, z)).x;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.position += Vector3.right * step;

            if (transform.position.x >= rightEdge)
            {
                movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            transform.position += Vector3.left * step;

            if (transform.position.x <= leftEdge)
            {
                movingRight = true;
                FlipSprite();
            }
        }
    }

    void FlipSprite()
    {
        // L?t hình n?u là nhân v?t sprite 2D
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
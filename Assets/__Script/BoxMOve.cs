using UnityEngine;

public class BoxMOve : MonoBehaviour
{
    public float speed = 2f;              // T?c ?? di chuy?n
    public float moveDistance = 5f;       // Kho?ng cách di chuy?n sang m?i bên

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float moveStep = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.position += Vector3.right * moveStep;
            if (transform.position.x >= startPosition.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveStep;
            if (transform.position.x <= startPosition.x - moveDistance)
                movingRight = true;
        }
    }
}

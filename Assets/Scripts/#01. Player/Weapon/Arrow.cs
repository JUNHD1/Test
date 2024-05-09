using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    public Vector2 Direction;

    private float moveSpeed = 5f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigid.velocity = Direction * moveSpeed;
    }
}

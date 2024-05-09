using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
  GameObject player; // 플레이어
  Rigidbody2D rigid;
  public float DistanceWithPlayer; // 플레이어와의 거리
  public Vector2 direction; // 방향

  private void Awake()
  {
    player = FindObjectOfType<PlayerMove>().gameObject;
    rigid = GetComponent<Rigidbody2D>();
  }

  public float ReturnDistanceWithPlayer()
  {
    DistanceWithPlayer = Vector2.Distance(player.transform.position, transform.position);
    return DistanceWithPlayer;
  }

  public Vector2 ReturnDirectionWithPlayer()
  {
    direction = transform.position - player.transform.position;
    return direction;
  }
}

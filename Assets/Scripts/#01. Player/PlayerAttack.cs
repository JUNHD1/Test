using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField] GameObject ArrowPrefab; // 화살 프리팹

  public void Attack(GameObject target, Vector2 targetDirection) // 타겟 오브젝트와 방향값 전달받음.
  {
    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 회전각 만들기
    GameObject Arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.Euler(0,0,angle - 90)); // 각도를 90도 틀어서 화살을 생성한다.
    Arrow.GetComponent<Arrow>().Direction = targetDirection;
  }
}

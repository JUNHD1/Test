using UnityEngine;

public class SetTarget : MonoBehaviour
{
  [SerializeField] private JoyStick JoyStick;
  private float CloseDistance = 99999999; // 엄청 넓은 거리로 초기화
  public Vector2 CloseDirection;
  public GameObject CloseMonster; // 제일 가까운 몬스터를 찾아서 담는 변수

  private void Update()
  {
    if(JoyStick.isSticking && CloseMonster == null) // 드래그중이라면. 굳이 타겟을 찾을 필요가 없다.
      return;
    FindTarget();
  }

  private void FindTarget() // 타겟을 찾는 함수(오브젝트와 거리, 방향을 찾음)
  {
    Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 3f, LayerMask.GetMask("Monster"));
    float tempDistance;
    CloseDistance = 99999999;
    CloseDirection = Vector2.zero;
    CloseMonster = null;
    //-------------------------------------------- 위에는 변수들을 초기화 하는것임 -------------------------------------------- 
    foreach(Collider2D coll in colls)
    {
      tempDistance = coll.gameObject.GetComponent<Monster>().ReturnDistanceWithPlayer(); // 플레이어와 몬스터 본인의 거리를 리턴받아서 제일 가까운 애를 찾아야됨
      if(tempDistance < CloseDistance) // 받아온 거리가 최종 거리보다 짧다면
      {
        CloseDistance = tempDistance;
        CloseMonster = coll.gameObject;
      }
    }
    if(CloseMonster == null) // 즉, 범위 안에는 아무도 없었다는거야.
      return;
    CloseDirection = CloseMonster.GetComponent<Monster>().ReturnDirectionWithPlayer(); // 방향을 정한다.
  }
}



/*
        rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, 2.0f);  
        if(hit.collider != null)
            Debug.Log(hit.collider.gameObject.name);  
*/
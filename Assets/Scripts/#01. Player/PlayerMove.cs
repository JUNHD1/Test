using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
  [SerializeField] GameObject[] playerObjs; // 플레이어 앞, 뒤, 좌, 우
  [SerializeField] float moveSpeed = 5f;
  [SerializeField] GameObject JoyStickVec; // 조이스틱 오브젝트
  SetTarget setTarget;
  Vector3 JoyVec; // JoyStickVec에서 JoyStickVec 변수를 호출하여 담는 역할
  public int NowplayerNum = 0; // 기준이 되는 플레이어의 방향(down - 0, up - 1, left - 2, right - 3)
  Rigidbody2D rigid;

  private void Awake() {
    rigid = GetComponent<Rigidbody2D>();
    setTarget = playerObjs[0].transform.parent.GetComponentInChildren<SetTarget>();
  }

  private void Update()
  {
    NowplayerNum = ChangePlayerVec(); // 조이스틱으로 캐릭터의 현재 방향을 찾는거죠
    for(int index = 0; index < playerObjs.Length; index++)
    {
      if(index == NowplayerNum) // 즉, 현재 방향에 맞는 인덱스다.
        playerObjs[index].SetActive(true); // 오브젝트 키고  
      else
        playerObjs[index].SetActive(false);
    }
  }

  private void FixedUpdate()
  {
    rigid.velocity = new Vector2(JoyVec.x, JoyVec.y) * moveSpeed; // 플레이어 이동
  }

  private int ChangePlayerVec() // 매 Update마다 호출. 조이스틱에 따른 캐릭터의 방향값을 파악해서 int형 NowplayerNum 변수에 대입시키고 있음
  {
    JoyVec = JoyStickVec.GetComponent<JoyStick>().JoyStickVec;
    int vec;
    
    if(setTarget.CloseMonster != null) // 지금 몬스터가 타겟으로 잡혔냐(조이스틱으로 움직이고 있을땐 안잡음)
    {
      if(Mathf.Abs(setTarget.CloseDirection.x) > Mathf.Abs(setTarget.CloseDirection.y)) // 좌우 이동값이 상하 이동값보다 더 크다.
        vec = setTarget.CloseDirection.x > 0 ? 3 : 2; // x > 0 크면 즉, 오른쪽이면 -3, 왼쪽이면 -2
      else if(Mathf.Abs(setTarget.CloseDirection.x) < Mathf.Abs(setTarget.CloseDirection.y)) // up > right
        vec = setTarget.CloseDirection.y > 0 ? 1 : 0; // y > 0 크면 즉, 위쪽이면 -1, 아래쪽이면 0
      else
        vec = 0; // 그냥 앞에 보게 만드는거야. 완전 멈춰있을 때
    }
    else // 몬스터가 타켓으로 안잡혔을 때
    {
      if(Mathf.Abs(JoyVec.x) > Mathf.Abs(JoyVec.y)) // 좌우 이동값이 상하 이동값보다 더 크다.
        vec = JoyVec.x > 0 ? 3 : 2; // x > 0 크면 즉, 오른쪽이면 -3, 왼쪽이면 -2
      else if(Mathf.Abs(JoyVec.x) < Mathf.Abs(JoyVec.y)) // up > right
        vec = JoyVec.y > 0 ? 1 : 0; // y > 0 크면 즉, 위쪽이면 -1, 아래쪽이면 0
      else if(setTarget.CloseMonster = null) // joyvec.x == joyvec.y 멈춘 경우인데 타켓이 잡혀있는 몬스터가 있냐 이거야
        vec = 1;
      else
        vec = 0; // 그냥 앞에 보게 만드는거야. 완전 멈춰있을 때
    }
    return vec;
  }
}

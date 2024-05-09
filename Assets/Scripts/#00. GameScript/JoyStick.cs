using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
  [SerializeField] GameObject bgStick; // 배경 스틱
  [SerializeField] GameObject Stick; // 조이스틱
  [SerializeField] GameObject player; // 플레이어
  private PlayerAttack playerAttack;
  private SetTarget setTarget; 
  public bool isSticking; // 조이스틱을 움직이고 있니?
  Animator animator;

  public Vector3 JoyStickVec; // 조이스틱 방향
  Vector3 InitJoyStickPos; // 조이스틱 최초 위치
  Vector3 JoyStickFirstMovePos; // 조이스틱 처음으로 움직인 위치(즉, 플레이어가 터치한 위치)
  private float Radius;

  private void Awake()
  {
    InitJoyStickPos = bgStick.transform.position; // 최초 조이스틱의 포지션을 저장해놓는다.
    Radius = bgStick.GetComponent<RectTransform>().sizeDelta.y / 2f; // 조이스틱 배경의 반지름을 구하는 변수
    animator = player.GetComponent<Animator>(); // 플레이어의 에니메이터를 가져옴
    playerAttack = player.GetComponent<PlayerAttack>();
    setTarget = player.GetComponentInChildren<SetTarget>();
    isSticking = false;
  }

  public void OnPointerDown(PointerEventData eventData) // 마우스를 포인터에 대고 딱 클릭한 시점
  {
    isSticking = true;
    bgStick.transform.position = Input.mousePosition;
    Stick.transform.position = Input.mousePosition;
    JoyStickFirstMovePos = Input.mousePosition; // 조이스틱이 최초로 이동할 포지션
    animator.SetInteger("State",3); // 걷는 모션
  }

  public void OnPointerUp(PointerEventData eventData) // 마우스 클릭이 끝난 시점(포지션이 동일하면 원점으로 복귀)
  {
    if(Stick.transform.position == Input.mousePosition) // 처음에 클릭했을때랑 똑같은거지
      ResetVec();
  }

  public void OnDrag(PointerEventData eventData) // 드래그 중일 때(즉, 조이스틱을 움직이고 있을 때)
  {
    isSticking = true;
    Vector3 DragPosition = eventData.position;
    JoyStickVec = (DragPosition - JoyStickFirstMovePos).normalized; // 조이스틱의 방향을 찾는 것

    float stickDistance = Vector3.Distance(DragPosition, JoyStickFirstMovePos); 

    if(stickDistance < Radius) // 반지름의 길이가 더 길다.
    {
      Stick.transform.position = JoyStickFirstMovePos + JoyStickVec * stickDistance;  // 거리가 반지름보다 작다 >>> 내가 터치하고 있는 곳으로 이동해야 함
    }
    else
    {
      Stick.transform.position = JoyStickFirstMovePos + JoyStickVec * Radius;
    }
  }

  public void OnEndDrag(PointerEventData eventData) // 드래그를 끝낼 때
  { 
    ResetVec();
  }

  public void OnDrop(PointerEventData eventData) // 드래그를 뗀 후(드랍 시)
  {
    ResetVec();
  }

  private void ResetVec() // 조이스틱 이동이 끝나면 원점으로 이동하는 코드
  {
    isSticking = false;
    JoyStickVec = Vector3.zero;
    bgStick.transform.position = InitJoyStickPos;
    Stick.transform.position = InitJoyStickPos;
    animator.SetInteger("State",0); // 멈추는 모션
  }

  private void Update()
  {
    if(isSticking || setTarget.CloseMonster == null) // 조이스틱을 쓰고 있거나 범위에 몬스터가 없으면
      return;
    GameObject TargetObject = setTarget.CloseMonster; // 가장 가까이에 있는 타겟 몬스터 오브젝트
    Vector2 TargetDirection = setTarget.CloseDirection; // 해당 오브젝트의 방향

    playerAttack.Attack(TargetObject, TargetDirection); // 멈췄으니 가장 가까이에 있는 적을 타겟으로 공격해야됨.(오브젝트, 방향값 인자로 전달)
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class CollectionJoker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    public GameObject ExplainBox;

    private void Awake()
    {
        // 필요한 컴포넌트 가져오기
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            // 박스 콜라이더 크기 자동 조정 (이미지 크기에 맞게)
            var image = GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                boxCollider.size = image.rectTransform.rect.size;
            }
        }
    }

    private bool isFirstEnable = true;

    private void Start()
    {
        // 메인 카메라 참조 가져오기
        mainCamera = Camera.main;
        // 초기 위치 저장
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        // 오브젝트가 활성화될 때마다 효과 재생
        PlayBounceEffect();
    }

    private void PlayBounceEffect()
    {
        // 젤리 같은 통통 튀는 효과
        transform.localScale = Vector3.zero;
        var image = GetComponent<UnityEngine.UI.Image>();
        
        // 투명도 초기화 (이미지가 있는 경우)
        if (image != null)
        {
            Color originalColor = image.color;
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }
        
        // 1.6 스케일로 커지면서 등장 (탄성 효과 있게)
        transform.DOScale(Vector3.one * 1.6f, 0.6f)
            .SetEase(Ease.OutElastic, 0.5f, 0.8f);
            
        // 투명도 애니메이션 (이미지가 있는 경우)
        if (image != null)
        {
            image.DOFade(1f, 0.4f);
        }
        
        // 약간의 회전 효과 추가 (더 동적인 느낌을 위해)
        transform.DORotate(new Vector3(0, 0, 5f), 0.2f, RotateMode.LocalAxisAdd)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
            
        // 약간의 흔들림 효과 (jelly 느낌 강조)
        transform.DOShakePosition(0.8f, 5f, 10, 90, false, false)
            .SetEase(Ease.OutElastic);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        
        isDragging = true;
        // ExplainBox 활성화
        if (ExplainBox != null)
        {
            ExplainBox.SetActive(true);
        }
        // 드래그 시작 시 레이캐스트 블록 해제
        boxCollider.enabled = false;
        // 드래그 중인 오브젝트를 가장 위 레이어로 이동
        transform.SetAsLastSibling();
        // 마우스 위치와 오브젝트 위치의 차이 계산
        Vector3 worldPoint;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out worldPoint);
        offset = transform.position - worldPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        
        // 마우스 위치로 오브젝트 이동
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out worldPoint))
        {
            // 부드러운 이동을 위해 DOTween 사용
            transform.DOMove(worldPoint + offset, 0.1f).SetEase(Ease.OutQuad);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        
        isDragging = false;
        // ExplainBox 비활성화
        if (ExplainBox != null)
        {
            ExplainBox.SetActive(false);
        }
        // 드래그 종료 시 레이캐스트 다시 활성화
        boxCollider.enabled = true;
        // 원래 위치로 돌아가는 애니메이션
        transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutBack);
        
        // 드래그가 끝나면 다시 원래의 하이라키 순서로 복원 (부모의 마지막 자식으로 이동)
        transform.SetAsLastSibling();
    }
}

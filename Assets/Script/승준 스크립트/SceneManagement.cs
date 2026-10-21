using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;
using System;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public GameObject StartBox;
    public GameObject IsReallyNewBox;
    public GameObject CollectionBox;
    public GameObject TutorialBox;
    public PlayerData playerData;
    public GameObject CreditBox;
    SaveManager saveManager;

    public TextMeshProUGUI planetText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI handCountText;
    public TextMeshProUGUI trashCountText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        playerData = saveManager.Load();
    }
    public void OnPlayBtnClick() 
    {
        // StartBox를 활성화
        StartBox.SetActive(true);
        
        // 초기 위치를 화면 아래로 설정
        RectTransform rectTransform = StartBox.GetComponent<RectTransform>();
        Vector3 startPos = rectTransform.anchoredPosition;
        startPos.y = -1300f; // 화면 아래로 1300만큼 내리기
        rectTransform.anchoredPosition = startPos;
        
        // DOTween을 사용하여 1300만큼 위로 부드럽게 올라오는 애니메이션
        rectTransform.DOAnchorPosY(0f, 1f)  // 1초 동안 y축 0 위치로 이동 (1300만큼 위로)
            .SetEase(Ease.OutBack);        // 탄성 효과 추가
        UpdateUI();
    }
    
    public void OnCollectionBtnClick()
    {
        if (CollectionBox == null) return;
        
        // CollectionBox 활성화
        CollectionBox.SetActive(true);
        
        // 모든 자식 요소의 CanvasGroup 가져오기 또는 생성
        var canvasGroups = CollectionBox.GetComponentsInChildren<CanvasGroup>(true);
        
        // 부드러운 페이드 인 효과 (0.5초 동안)
        foreach (var canvasGroup in canvasGroups)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
        }
        
        // 메인 이미지도 페이드 인 (이미지가 있는 경우)
        var image = CollectionBox.GetComponent<UnityEngine.UI.Image>();
        if (image != null)
        {
            Color originalColor = image.color;
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            image.DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
        }
    }
    public void OnTutorialBtnClick()
    {
        // TutorialBox를 활성화
        TutorialBox.SetActive(true);
        
        // RectTransform 가져오기
        RectTransform rectTransform = TutorialBox.GetComponent<RectTransform>();
        
        // 현재 위치에서 1100만큼 위로 이동
        Vector2 targetPos = rectTransform.anchoredPosition;
        targetPos.y += 1100f;
        
        // 부드러운 이동 애니메이션
        rectTransform.DOAnchorPos(targetPos, 0.5f)
            .SetEase(Ease.OutQuad);
    }
    public void OnTutorialCloseBtnClick()
    {
        if (TutorialBox == null || !TutorialBox.activeSelf) return;
        
        // RectTransform 가져오기
        RectTransform rectTransform = TutorialBox.GetComponent<RectTransform>();
        
        // 현재 위치에서 1100만큼 아래로 이동
        Vector2 targetPos = rectTransform.anchoredPosition;
        targetPos.y -= 1100f;
        
        // 부드러운 이동 애니메이션 후 비활성화
        rectTransform.DOAnchorPos(targetPos, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                if (TutorialBox != null)
                {
                    TutorialBox.SetActive(false);
                }
            });
    }
public void OnCollectionCloseBtnClick()
{
    if (CollectionBox == null || !CollectionBox.activeSelf) return;
    
    // 모든 자식 요소의 CanvasGroup 가져오기
    var canvasGroups = CollectionBox.GetComponentsInChildren<CanvasGroup>(true);
    
    // 부드러운 페이드 아웃 효과 (0.4초 동안)
    foreach (var canvasGroup in canvasGroups)
    {
        canvasGroup.DOFade(0f, 0.4f).SetEase(Ease.OutQuad);
    }
    
    // 메인 이미지도 페이드 아웃 (이미지가 있는 경우)
    var image = CollectionBox.GetComponent<UnityEngine.UI.Image>();
    if (image != null)
    {
        image.DOFade(0f, 0.4f).SetEase(Ease.OutQuad);
    }
    
    // 페이드 아웃이 끝난 후 비활성화
    DOVirtual.DelayedCall(0.4f, () => {
        if (CollectionBox != null)
        {
            CollectionBox.SetActive(false);
        }
    });
}
    


public void onNewStartBtnClick()
{
    // 오브젝트 활성화
    IsReallyNewBox.SetActive(true);
    
    // RectTransform 가져오기
    RectTransform rectTransform = IsReallyNewBox.GetComponent<RectTransform>();
    
    // 시작 위치 설정 (화면 오른쪽, y = 71)
    Vector3 startPos = new Vector3(950f, 71f, 0f);
    rectTransform.anchoredPosition = startPos;
    
    // 목표 위치 (-50, 71, 0)
    Vector3 targetPos = new Vector3(-50f, 71f, 0f);
    
    // 모바일 최적화:
    // 1. Ease.OutQuad 사용 (OutBack보다 가볍고 부드러움)
    // 2. 애니메이션 시간 0.8초 → 0.6초로 단축
    // 3. SetUpdate로 모바일 최적화
    rectTransform.DOAnchorPos(targetPos, 0.6f)
        .SetEase(Ease.OutQuad)
        .SetUpdate(UpdateType.Normal, true)  // 모바일에서 더 부드러운 애니메이션
        .SetLink(gameObject);  // 메모리 누수 방지
}
public void XBtnClick()
{
    // RectTransform 가져오기
    RectTransform rectTransform = StartBox.GetComponent<RectTransform>();
    
    // 현재 위치에서 시작
    Vector3 currentPos = rectTransform.anchoredPosition;
    
    // 목표 위치는 아래로 950 이동 (y는 현재 위치에서 -950)
    Vector3 targetPos = new Vector3(currentPos.x, currentPos.y - 950f, 0f);
    
    // DOTween을 사용하여 아래로 부드럽게 이동한 후 비활성화
    rectTransform.DOAnchorPos(targetPos, 0.8f)  // 0.8초 동안 아래로 이동
        .SetEase(Ease.InBack)                  // 안쪽으로 들어가는 느낌의 이징
        .OnComplete(() => {
            StartBox.SetActive(false);    // 애니메이션 완료 후 비활성화
        });
}
public void YesBtnClick()
{
        // 플레이어 데이터 초기화
        playerData = new PlayerData();
        playerData.money = 20;
        playerData.handcount = 4;
        playerData.trashcount = 4;
        playerData.round = 1;
        playerData.ante = 1;
        playerData.bestscore = 0;
        playerData.moneyLimit = 5;
        
        // 조커 아이템 초기화
        ResetJokerItems();
        saveManager.Save(playerData);
        SceneManager.LoadScene("Stage");
        JokerChipStack.Instance.ResetAllChips();

}
private void ResetJokerItems()
{
    try
    {
        string savePath = "./Saves/JokerZone";
        string filePath = Path.Combine(savePath, "JokerZoneData.json");
        
        // 디렉토리가 없으면 생성
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        
        // 파일이 존재하면 삭제
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"[SceneManagement] 조커 아이템 데이터가 초기화되었습니다: {filePath}");
        }
        
        // 현재 씬의 조커 아이템 오브젝트도 제거
        var jokerZone = GameObject.FindGameObjectWithTag("JokerZone");
        if (jokerZone != null)
        {
            // JokerZone의 모든 자식 오브젝트 제거
            foreach (Transform child in jokerZone.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        // ItemData 컴포넌트의 데이터도 초기화
        var itemData = FindObjectOfType<ItemData>();
        if (itemData != null)
        {
            // 리플렉션을 사용하여 private 필드 초기화
            var field = itemData.GetType().GetField("buyJokersData", 
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                // BuyJokersData 클래스의 인스턴스 생성 (필요시 using 추가)
                var buyJokersData = Activator.CreateInstance(Type.GetType("BuyJokersData"));
                field.SetValue(itemData, buyJokersData);
            }
        }
    }
    catch (Exception e)
    {
        Debug.LogError($"[SceneManagement] 조커 아이템 초기화 중 오류 발생: {e.Message}");
    }
}
public void NoBtnClick()
{
    if (IsReallyNewBox == null) return;
    
    // RectTransform 가져오기
    RectTransform rectTransform = IsReallyNewBox.GetComponent<RectTransform>();
    
    // 현재 위치에서 시작
    Vector3 startPos = rectTransform.anchoredPosition;
    
    // 목표 위치는 오른쪽으로 이동 (x: 950)
    Vector3 targetPos = new Vector3(950f, 71f, 0f);
    
    // DOTween을 사용하여 오른쪽으로 부드럽게 이동한 후 비활성화
    rectTransform.DOAnchorPos(targetPos, 0.8f)  // 0.8초 동안 오른쪽으로 이동
        .SetEase(Ease.InBack)                  // 안쪽으로 들어가는 느낌의 이징
        .OnComplete(() => {
            IsReallyNewBox.SetActive(false);    // 애니메이션 완료 후 비활성화
        });
}
    public void onContinueBtnClick()
    {
        SceneManager.LoadScene("Stage");
    }

    public void onCreditBtnClick()
    {
        if (CreditBox == null) return;
        
        // CreditBox 활성화
        CreditBox.SetActive(true);
        
        // RectTransform 가져오기
        RectTransform rectTransform = CreditBox.GetComponent<RectTransform>();
        
        // 초기 위치를 화면 아래로 설정 (y축 -1350)
        Vector3 startPos = rectTransform.anchoredPosition;
        startPos.y = -1350f;
        rectTransform.anchoredPosition = startPos;
        
        // DOTween을 사용하여 y축 -160 위치로 부드럽게 올라오는 애니메이션
        rectTransform.DOAnchorPosY(-160f, 0.8f)  // 0.8초 동안 y축 -160 위치로 이동
            .SetEase(Ease.OutBack);              // 탄성 효과 추가
    }
    public void onCreditXBtnClick()
    {
        if (CreditBox == null || !CreditBox.activeSelf) return;
        
        // RectTransform 가져오기
        RectTransform rectTransform = CreditBox.GetComponent<RectTransform>();
        
        // DOTween을 사용하여 y축 -1350 위치로 부드럽게 내려가는 애니메이션
        rectTransform.DOAnchorPosY(-1350f, 0.6f)  // 0.6초 동안 y축 -1350 위치로 이동
            .SetEase(Ease.InBack)                // 안쪽으로 들어가는 느낌의 이징
            .OnComplete(() => {
                CreditBox.SetActive(false);       // 애니메이션 완료 후 비활성화
            });
    }
    public void UpdateUI()
    {    
        string planetName = playerData.ante switch
        {
        1 => "수성",
        2 => "금성",
        3 => "지구",
        4 => "달",
        5 => "화성",
        6 => "목성",
        7 => "토성",
        8 => "천왕성",
        9 => "해왕성",
        10 => "명왕성",
        _ => playerData.ante.ToString()  // 기본값 (1-10 이외의 숫자일 경우)
    };
    
    planetText.text = "행성 : " + planetName;
        roundText.text = "라운드 : " + playerData.round.ToString();
        handCountText.text = "핸드플레이 : " + playerData.handcount.ToString();
        trashCountText.text = "버리기 : " + playerData.trashcount.ToString();
        moneyText.text = "보유한 돈 : $" + playerData.money.ToString();
        bestScoreText.text = "최고점수 : " + playerData.bestscore.ToString();
    }
}

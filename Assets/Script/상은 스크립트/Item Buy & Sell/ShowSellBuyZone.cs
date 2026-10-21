using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShowSellBuyZone : MonoBehaviour
{
    public GameObject BuyZone;
    public GameObject SellZone;
    
    // BuyZone을 활성화할 태그 목록
    private readonly HashSet<string> buyValidTags = new HashSet<string>
    {
        "Joker",
        "ItemPack",
        "Planet",
        "Taro",
        "Voucher",
        "MoneyPack"
    };
    
    // SellZone을 활성화할 태그 (BuyJoker만 체크)
    private const string SELL_ZONE_TAG = "BuyJoker";

    private void Start()
    {
        // 초기 상태 설정
        if (BuyZone != null) BuyZone.SetActive(false);
        if (SellZone != null) SellZone.SetActive(false);
    }

    void Update()
    {
        // 매 프레임 모든 DragItem 컴포넌트 찾기
        var dragItems = FindObjectsOfType<DragItem>();
        
        if (dragItems.Length == 0)
        {
            // 드래그 중인 아이템이 없으면 모든 존 비활성화
            if (BuyZone != null && BuyZone.activeSelf) BuyZone.SetActive(false);
            if (SellZone != null && SellZone.activeSelf) SellZone.SetActive(false);
            return;
        }

        // 모든 DragItem 중 유효한 태그를 가진 아이템이 드래그 중인지 확인
        var draggingItem = dragItems.FirstOrDefault(item => 
            item.isDrag && !string.IsNullOrEmpty(item.currentTag));
            
        if (draggingItem != null)
        {
            // BuyZone 또는 SellZone 중 어떤 것을 보여줄지 결정
            if (buyValidTags.Contains(draggingItem.currentTag))
            {
                // BuyZone 활성화, SellZone 비활성화
                if (BuyZone != null) BuyZone.SetActive(true);
                if (SellZone != null) SellZone.SetActive(false);
            }
            else if (draggingItem.currentTag == SELL_ZONE_TAG)
            {
                // SellZone 활성화, BuyZone 비활성화
                if (SellZone != null) SellZone.SetActive(true);
                if (BuyZone != null) BuyZone.SetActive(false);
            }
            else
            {
                // 유효한 태그가 아닌 경우 모두 비활성화
                if (BuyZone != null) BuyZone.SetActive(false);
                if (SellZone != null) SellZone.SetActive(false);
            }
        }
        else
        {
            // 드래그 중이 아닌 경우 모두 비활성화
            if (BuyZone != null) BuyZone.SetActive(false);
            if (SellZone != null) SellZone.SetActive(false);
        }
    }
}
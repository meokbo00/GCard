using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public Button NextBtn;
    public Button BackBtn;
    public GameObject[] CollectionPanel;
    
    private int currentPanelIndex = 0;

    void Start()
    {
        // 초기 상태 설정
        UpdatePanelVisibility();
        
        // 버튼 클릭 이벤트 연결
        if (NextBtn != null)
            NextBtn.onClick.AddListener(NextPanel);
            
        if (BackBtn != null)
            BackBtn.onClick.AddListener(PreviousPanel);
            
        // 초기 버튼 상태 업데이트
        UpdateButtonInteractivity();
    }

    public void NextPanel()
    {
        if (currentPanelIndex < CollectionPanel.Length - 1)
        {
            currentPanelIndex++;
            UpdatePanelVisibility();
            UpdateButtonInteractivity();
        }
    }

    public void PreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            UpdatePanelVisibility();
            UpdateButtonInteractivity();
        }
    }

    private void UpdatePanelVisibility()
    {
        // 모든 패널 비활성화
        for (int i = 0; i < CollectionPanel.Length; i++)
        {
            if (CollectionPanel[i] != null)
            {
                CollectionPanel[i].SetActive(i == currentPanelIndex);
            }
        }
    }

    private void UpdateButtonInteractivity()
    {
        // 다음 버튼 비활성화: 마지막 패널일 때
        if (NextBtn != null)
            NextBtn.interactable = (currentPanelIndex < CollectionPanel.Length - 1);
            
        // 이전 버튼 비활성화: 첫 번째 패널일 때
        if (BackBtn != null)
            BackBtn.interactable = (currentPanelIndex > 0);
    }
}
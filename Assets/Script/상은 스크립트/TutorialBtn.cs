using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBtn : MonoBehaviour
{
    public Button[] tutorialButtons;  // 버튼 배열
    public GameObject[] tutorialObjects;  // 활성화/비활성화할 오브젝트 배열

    void Start()
    {
        // 버튼 클릭 이벤트 등록
        for (int i = 0; i < tutorialButtons.Length; i++)
        {
            int index = i;  // 클로저를 위해 현재 인덱스 저장
            tutorialButtons[i].onClick.AddListener(() => OnTutorialButtonClick(index));
        }
    }

    // 버튼 클릭 시 호출될 메서드
    private void OnTutorialButtonClick(int buttonIndex)
    {
        // 모든 오브젝트 비활성화
        foreach (var obj in tutorialObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        // 선택된 인덱스의 오브젝트만 활성화 (유효한 인덱스인지 확인)
        if (buttonIndex >= 0 && buttonIndex < tutorialObjects.Length && tutorialObjects[buttonIndex] != null)
        {
            tutorialObjects[buttonIndex].SetActive(true);
        }
    }
}

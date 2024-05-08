using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public TMP_Text dialogueText;
    public Button nextBtn;  // 다음 대화로 넘어가는 버튼
    public List<Scenario> dialogues;

    private int curIndex = 0;
    private Coroutine typingCoroutine;

    public void Start()
    {
        // 다음 대화로 넘어가는 버튼에 클릭 이벤트 추가
        nextBtn.onClick.AddListener(PrintDialogue);

        dialogues = GameManager.instance.scenarioObject.dataList;
    }

    public void PrintDialogue()
    {
        if (curIndex < dialogues.Count)    // 대화가 모두 출력되었는지 확인
        {
            curIndex++;
            if (typingCoroutine != null)
            {
                // 타이핑 중이라면 즉시 완료하고 다음 대사로 넘어감
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogues[curIndex - 1].content;
            }
            else
            {
                // 타이핑 중이 아니라면 다음 대사를 타이핑 효과와 함께 출력
                typingCoroutine = StartCoroutine(Typing(dialogues[curIndex - 1].content));
            }
        }
    }

    IEnumerator Typing(string dialogue)    
    {
        // 타이핑 효과 함수

        dialogueText.text = ""; // 텍스트 초기화

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 텍스트 출력 속도 조절
        }

        typingCoroutine = null;
    }
}

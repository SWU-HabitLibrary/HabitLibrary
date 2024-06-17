using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public TMP_Text dialogueText;
    public Button nextBtn;                      // ���� ��ȭ�� �Ѿ�� ��ư
    public List<Scenario> dialogues;

    private int curIndex = 0;
    private Coroutine typingCoroutine;

    public void Start()
    {
        // ���� ��ȭ�� �Ѿ�� ��ư�� Ŭ�� �̺�Ʈ �߰�
        nextBtn.onClick.AddListener(OnNextButtonClick);

        dialogues = GameManager.Instance.scenarioObject.dataList;
    }
    private void OnNextButtonClick()
    {
        PrintDialogue();
    }

    public void PrintDialogue()
    {
        if (curIndex < dialogues.Count)
        {
            if (typingCoroutine != null)
            {
                // Ÿ���� ���̶�� ��� �Ϸ��ϰ� ���� ���� �Ѿ
                StopTypingCoroutine();
            }
            else
            {
                // Ÿ���� ���� �ƴ϶�� ���� ��縦 Ÿ���� ȿ���� �Բ� ���
                typingCoroutine = StartCoroutine(Typing(dialogues[curIndex].content));
                curIndex++;
            }
        }
    }

    private void StopTypingCoroutine()
    {
        StopCoroutine(typingCoroutine);
        dialogueText.text = dialogues[curIndex].content;
        typingCoroutine = null;
        curIndex++;
    }

    IEnumerator Typing(string dialogue)    
    {
        // Ÿ���� ȿ�� �Լ�
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // �ؽ�Ʈ ��� �ӵ� ����
        }

        typingCoroutine = null;
    }
}

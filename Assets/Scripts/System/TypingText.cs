using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public TMP_Text dialogueText;
    public Button nextBtn;  // ���� ��ȭ�� �Ѿ�� ��ư
    public List<Scenario> dialogues;

    private int curIndex = 0;
    private Coroutine typingCoroutine;

    public void Start()
    {
        // ���� ��ȭ�� �Ѿ�� ��ư�� Ŭ�� �̺�Ʈ �߰�
        nextBtn.onClick.AddListener(PrintDialogue);

        dialogues = GameManager.instance.scenarioObject.dataList;
    }

    public void PrintDialogue()
    {
        if (curIndex < dialogues.Count)    // ��ȭ�� ��� ��µǾ����� Ȯ��
        {
            curIndex++;
            if (typingCoroutine != null)
            {
                // Ÿ���� ���̶�� ��� �Ϸ��ϰ� ���� ���� �Ѿ
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogues[curIndex - 1].content;
            }
            else
            {
                // Ÿ���� ���� �ƴ϶�� ���� ��縦 Ÿ���� ȿ���� �Բ� ���
                typingCoroutine = StartCoroutine(Typing(dialogues[curIndex - 1].content));
            }
        }
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

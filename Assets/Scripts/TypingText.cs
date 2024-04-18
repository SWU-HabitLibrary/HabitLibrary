using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingText : MonoBehaviour
{

    public TMP_Text tempText;
    public string dialogue;

    public void printText()
    {
        dialogue = "���⼭ ��ȭ �ؽ�Ʈ ���";

        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string _text)
    {
        // �ؽ�Ʈ�� null ������ ����
        tempText.text = null;

        for (int i = 0; i < _text.Length; i++)
        {
            tempText.text += _text[i];
            yield return new WaitForSeconds(0.08f); // �ӵ� ����
        }
    }
}

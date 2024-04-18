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
        dialogue = "여기서 대화 텍스트 출력";

        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string _text)
    {
        // 텍스트를 null 값으로 설정
        tempText.text = null;

        for (int i = 0; i < _text.Length; i++)
        {
            tempText.text += _text[i];
            yield return new WaitForSeconds(0.08f); // 속도 조절
        }
    }
}

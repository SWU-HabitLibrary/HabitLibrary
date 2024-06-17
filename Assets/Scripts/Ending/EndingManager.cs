using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public Image fadeImage;          // 페이드 효과를 위한 이미지
    public Text typingText;          // 타이핑 효과를 위한 텍스트
    public GameObject endingImage;   // 엔딩 이미지 오브젝트

    private string[] endingTexts = {
        "이것은 끝이 아니라 새로운 시작입니다.",
        "실버 리프의 수호자는 계속해서 지식과 마법의 불꽃을 밝힐 것입니다."
    };
    private string endingAchievedMessage = "엔딩을 획득하였습니다.";

    private void Start()
    {
        typingText.text = "";
        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // 페이드인 효과
        yield return StartCoroutine(FadeIn());

        // 타이핑 효과
        foreach (string text in endingTexts)
        {
            yield return StartCoroutine(TypeText(text));
            yield return new WaitForSeconds(1.0f); // 다음 텍스트 출력 전 잠시 대기
        }

        // 엔딩 이미지 페이드인 효과
        yield return StartCoroutine(FadeInEndingImage());

        // 엔딩 획득 텍스트 출력
        yield return StartCoroutine(TypeText(endingAchievedMessage));

        // 타이틀 화면으로 이동
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Title");
    }

    private IEnumerator FadeIn()
    {
        float duration = 2.0f;
        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentTime / duration);
            fadeImage.color = new Color(0, 0, 0, 1 - alpha);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        typingText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            typingText.text += letter;
            yield return new WaitForSeconds(0.05f); // 타이핑 속도 조절
        }

        yield return new WaitForSeconds(1.0f); // 다음 텍스트 출력 전 잠시 대기
    }

    private IEnumerator FadeInEndingImage()
    {
        endingImage.SetActive(true);
        Image imageComponent = endingImage.GetComponent<Image>();
        float duration = 2.0f;
        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentTime / duration);
            imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, alpha);
            yield return null;
        }
    }
}

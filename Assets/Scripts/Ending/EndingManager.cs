using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public Image fadeImage;          // ���̵� ȿ���� ���� �̹���
    public Text typingText;          // Ÿ���� ȿ���� ���� �ؽ�Ʈ
    public GameObject endingImage;   // ���� �̹��� ������Ʈ

    private string[] endingTexts = {
        "�̰��� ���� �ƴ϶� ���ο� �����Դϴ�.",
        "�ǹ� ������ ��ȣ�ڴ� ����ؼ� ���İ� ������ �Ҳ��� ���� ���Դϴ�."
    };
    private string endingAchievedMessage = "������ ȹ���Ͽ����ϴ�.";

    private void Start()
    {
        typingText.text = "";
        StartCoroutine(PlayEndingSequence());
    }

    private IEnumerator PlayEndingSequence()
    {
        // ���̵��� ȿ��
        yield return StartCoroutine(FadeIn());

        // Ÿ���� ȿ��
        foreach (string text in endingTexts)
        {
            yield return StartCoroutine(TypeText(text));
            yield return new WaitForSeconds(1.0f); // ���� �ؽ�Ʈ ��� �� ��� ���
        }

        // ���� �̹��� ���̵��� ȿ��
        yield return StartCoroutine(FadeInEndingImage());

        // ���� ȹ�� �ؽ�Ʈ ���
        yield return StartCoroutine(TypeText(endingAchievedMessage));

        // Ÿ��Ʋ ȭ������ �̵�
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
            yield return new WaitForSeconds(0.05f); // Ÿ���� �ӵ� ����
        }

        yield return new WaitForSeconds(1.0f); // ���� �ؽ�Ʈ ��� �� ��� ���
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

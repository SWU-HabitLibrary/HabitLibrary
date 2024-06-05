using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // ĳ���� ������ �ִϸ��̼�
    Animator animator;
    private bool isMoving = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        while (true)
        {
            if (isMoving)
            {
                // �ȱ� �ִϸ��̼� ���
                PlayRandomWalkAnimation();
                yield return new WaitForSeconds(1.5f);
                animator.SetBool("isMove", false);
            }
            else
            {
                // ������ �ִ� �ִϸ��̼� ���
                PlayRandomIdleAnimation();
                yield return new WaitForSeconds(3f);
                animator.SetBool("isMove", true);
            }

            isMoving = !isMoving; // �ȱ�� ������ �ֱ⸦ �����ư��� ����
        }
    }
    private void PlayRandomWalkAnimation()
    {
        float randomWalk = Random.Range(0f, 4f); // 0.0���� 3.9���� ������ ���� ����
        animator.SetFloat("move_index", randomWalk);
    }

    private void PlayRandomIdleAnimation()
    {
        float randomIdle = Random.Range(0f, 4f); // 0.0���� 3.9���� ������ ���� ����
        animator.SetFloat("idle_index", randomIdle);
    }
}

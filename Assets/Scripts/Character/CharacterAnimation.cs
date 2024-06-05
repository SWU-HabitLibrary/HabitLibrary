using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // 캐릭터 움직임 애니메이션
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
                // 걷기 애니메이션 재생
                PlayRandomWalkAnimation();
                yield return new WaitForSeconds(1.5f);
                animator.SetBool("isMove", false);
            }
            else
            {
                // 가만히 있는 애니메이션 재생
                PlayRandomIdleAnimation();
                yield return new WaitForSeconds(3f);
                animator.SetBool("isMove", true);
            }

            isMoving = !isMoving; // 걷기와 가만히 있기를 번갈아가며 실행
        }
    }
    private void PlayRandomWalkAnimation()
    {
        float randomWalk = Random.Range(0f, 4f); // 0.0부터 3.9까지 랜덤한 숫자 생성
        animator.SetFloat("move_index", randomWalk);
    }

    private void PlayRandomIdleAnimation()
    {
        float randomIdle = Random.Range(0f, 4f); // 0.0부터 3.9까지 랜덤한 숫자 생성
        animator.SetFloat("idle_index", randomIdle);
    }
}

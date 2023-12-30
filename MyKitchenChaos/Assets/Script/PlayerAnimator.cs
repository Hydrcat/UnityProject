using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const String IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;
 
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}

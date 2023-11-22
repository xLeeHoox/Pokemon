using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Player : MonoBehaviour
{
    [HideInInspector] public MoveByVelocity moveByVelocity;
    [HideInInspector] public CharacterSO playerSO;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public DashAbility dashAbility;
    [HideInInspector] public PowerController powerController;
    [HideInInspector] public Score score;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public AnimationChange animationChange;
    [SerializeField] SpriteRenderer playerImage;
    [SerializeField] Transform collectArea;
    [SerializeField] float collectAreaRadius;
    [SerializeField] public Transform dashEffectParent;
    [SerializeField] public Transform petParent;

    [HideInInspector] public bool isPlayerDead = false; // su dung de xu ly effect luc playerDead

    public static int killNumber;



    public void Awake()
    {
        playerSO = GameResources.Instance.currentCharacterSO;
        moveByVelocity = GetComponent<MoveByVelocity>();
        dashAbility = GetComponent<DashAbility>();
        rb = GetComponent<Rigidbody2D>();
        powerController = GetComponentInChildren<PowerController>();
        score = GetComponent<Score>();
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
        animationChange = GetComponent<AnimationChange>();
        killNumber = 0;
    }
    public void UpdateImage(Sprite sprite)
    {
        playerImage.sprite = sprite;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(collectArea.position, collectAreaRadius);
    }
    public List<Vector3> GetRewardInArea()
    {
        List<Vector3> result = new List<Vector3>();
        List<Collider2D> collider2Ds = Physics2D.OverlapCircleAll(collectArea.position, collectAreaRadius).ToList();
        foreach (var item in collider2Ds)
        {
            CollectableObject collectableObject = item.GetComponent<CollectableObject>();
            if (collectableObject != null)
            {
                result.Add(collectableObject.transform.position);
            }
        }
        return result;
    }
}

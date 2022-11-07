using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCManagerBase : MonoBehaviour, INPCManager
{
    [Header("World Settings")]
    public float gravityScale = 0f;
    [Header("Player Settings")]
    [SerializeField]
    private NPCAttributesSO npcAttributes;
    [SerializeField]
    private NPCType npcType;

    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public float moveDirection = 0;
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public CapsuleCollider2D mainCollider;
    [HideInInspector] public Transform thisTransform;
    [HideInInspector] public bool facingRight = true;

    public void SetNPC()
    {
        thisTransform = transform;
        mainCollider = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2d.gravityScale = gravityScale;
        facingRight = thisTransform.localScale.x > 0;

    }

    public void Update()
    {
        
    }

    public void MoveNPC(NPCType npcType, InputPlayer inputPlayer = null)
    {
        switch (npcType)
        {
            case NPCType.NPC:
                MoveToNextPoint();
                break;
            case NPCType.Player:
                MovePlayer();
                break;
            case NPCType.Enemy:
                break;
            case NPCType.Boss:
                break;
        }


    }

    public void JumpController(NPCType npcType, InputPlayer inputPlayer = null)
    {
        bool canJump = false;
        switch (npcType)
        {
            case NPCType.NPC:
                break;
            case NPCType.Player:
                if (inputPlayer != null)
                {
                    if (inputPlayer.isJump && isGrounded)
                    {
                        canJump = true;
                    }
                }
                break;
            case NPCType.Enemy:
                if (isGrounded)
                {
                    canJump = true;
                }
                break;
            case NPCType.Boss:
                break;
        }

        if (canJump)
        {
            canJump = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, GetCharacterAttributes().GetJumpPower());
        }

    }

    public void GroundCheckPosition()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos =
            colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        rb2d.velocity = new Vector2((moveDirection) * npcAttributes.GetSpeedUser(), rb2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0),
            isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0),
            isGrounded ? Color.green : Color.red);
    }

    public void MoveToTarget(Vector2 dirToTarget)
    {
        transform.position += (Vector3)dirToTarget *
                              (npcAttributes.speedUser * Time.deltaTime);
    }



    public NPCAttributesSO GetCharacterAttributes()
    {
        return npcAttributes;
    }
    
    virtual public void MovePlayer()
    {
    }

    virtual public void MoveToNextPoint()
    {
    }
}

public enum NPCType
{
    NPC = 0,
    Player = 1,
    Enemy = 2,
    Boss = 3
}
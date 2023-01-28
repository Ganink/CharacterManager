using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HitBoxItem))]
[RequireComponent(typeof(EnemyInput))]
[RequireComponent(typeof(CircleCollider2D))]
public class EnemyManager : NPCManagerBase
{
    public float rangeToAction = 3f;

    private bool isDeath;
    private bool isAttackEnable;
    private bool isCombatEnable;

    private Vector2 dirAttack;

    private EnemyInput EnemyInput;
    private AttackManager AttackController;
    private Animator Animator;
    private SpriteRenderer sprite;
    private CircleCollider2D cirCollider;

    [SerializeField] private float rangeDetection;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float probabilityAttack;

    //hashcode to animations
    private int walkHash;
    private int attackHash;
    private int blockkHash;

    protected void Start()
    {
        base.Start();
        Initialized();
    }

    private void Update() {
        EnemyLogic();
    }

    private void Initialized() {
        AttackController = GetComponent<AttackManager>();
        Animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        EnemyInput = GetComponent<EnemyInput>();
        cirCollider = GetComponent<CircleCollider2D>();
        cirCollider.isTrigger = true;
        cirCollider.radius = rangeToAction;
        //hashCode
        //NewHashCodeForAnimations();

        //stats
        if (probabilityAttack == 0) probabilityAttack = 95;
    }

    // HashCode
    private void NewHashCodeForAnimations() {
        //hash
        walkHash = Animator.StringToHash("IsWalk");
        attackHash = Animator.StringToHash("AttackEnable");
        blockkHash = Animator.StringToHash("BlockEnable");
    }

    // Logic
    private void EnemyLogic() {
        if (!isDeath) {
            // attack
            if (!isAttackEnable && rangeToAction < rangeAttack) {
                EnemyAttackLogic();
            }
            else if (!isAttackEnable && (isCombatEnable || rangeToAction < rangeDetection)) {
                MoveToTarget();
            }
            else if (!isAttackEnable && !isCombatEnable && rangeToAction > rangeDetection) {
                //Animator.SetBool(walkHash, false);
            }
        }
    }

    private void EnemyAttack() {
        AttackController.PlayerAttack(characterController.GetCharacterAttributes().Strong, dirAttack);
        // AttackIsDisabled(); // when this call is a comment, this method will be called in the animation clip.
    }

    private void EnemyAttackLogic() {

        int p_probabilityAttack = Random.Range(0, 100); // percent
        if (p_probabilityAttack > probabilityAttack) {
            dirAttack = EnemyInput.dirToTarget;
            isAttackEnable = true;
            isCombatEnable = true;
            //Animator.SetBool(walkHash, false);
            //Animator.SetTrigger(attackHash); // maybe use hashCode?
            //EnemyAttack(); // when this call is a comment, this method will be called in the animation clip.
        }

    }

    private void MoveToTarget() {
        //Animator.SetBool(walkHash, true);
        FlipSprite();
        transform.position += (Vector3)EnemyInput.dirToTarget * characterController.GetCharacterAttributes().Speed * Time.deltaTime;
    }

    private void FlipSprite() {
        if (EnemyInput.horizontal < 0) {
            sprite.flipX = true;
        }
        else {
            sprite.flipX = false;
        }
    }

    private void AttackIsDisabled() {
        isAttackEnable = false;
    }

    #region EDITOR_CONTROL
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }

    #endregion
}

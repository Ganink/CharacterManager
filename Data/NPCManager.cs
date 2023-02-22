using System;
using Greenvillex.RepositoryManager;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(UserSettings))]
[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AttackManager))]
public class NPCManager : NPCManagerBase
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] bool canMove;
    [SerializeField] AttackManager attackManager;

#if PLATFORM_IOS || UNITY_ANDROID
    [SerializeField]
    private JoystickManager joystick;
#endif

    private InputPlayer inputPlayer;

    private float Horizontal;
    private float Vertical;
    private SpriteRenderer playerSprite;
    private Animator animator;
    private int hasCodeRun;

    protected void Start()
    {
        base.Start();
        Initialized();
    }

    private void Initialized()
    {
        if (GetNPCType() == NPCType.Player)
        {
            SetNPC();
            inputPlayer = GetComponent<InputPlayer>();
            mainCamera = GetCamera();
            characterController.SetMainCamera(this.transform);
            MinimapSettings.Instance.SetMinimap(this.transform); //need abstract more this call
            playerSprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            attackManager = GetComponent<AttackManager>();
            canMove = true;
            //hasCodeRun = Animator.StringToHash("walk");

#if PLATFORM_IOS || UNITY_ANDROID
            joystick = FindObjectOfType<JoystickManager>(true);
            joystick.gameObject.SetActive(true);
            MobileAction();
#endif
        }
    }

    private GameObject GetCamera()
    {
        var copyCamera = Instantiate(mainCamera, this.transform);
        return copyCamera;
    }

    public void SetCanMove(bool state)
    {
        canMove = state;
        Debug.Log($"[NPCManager]: Can Move?: {canMove}");
    }

    protected void Update()
    {
        base.Update();
#if PHOTON_UNITY_NETWORKING
        if (PhotonNetwork.InRoom && !gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (gameObject.GetComponentInChildren<PanelChat>() != null)
            {
                gameObject.GetComponentInChildren<PanelChat>().gameObject.SetActive(false);
            }
            mainCamera.SetActive(false);
            return;
        }
#endif

        MoveNPC(NPCType.Player);
    }

    public override void MovePlayer()
    {
        if (GetNPCType() == NPCType.Player)
        {
            if (canMove)
            {
                base.MovePlayer();
                Vector3 mov = MovePlayerFlow();
            }

            SetNPCAnimation();
        }

        FlipSprite();
    }

    private Vector3 MovePlayerFlow()
    {
#if PLATFORM_IOS || UNITY_ANDROID
        Vector3 mov = Flow(joystick.Horizontal, joystick.Vertical);
#else
        Vector3 mov = Flow(inputPlayer.axisHorizontal, inputPlayer.axisVertical);
        if (inputPlayer.isAttack)
        {
            animator.SetTrigger("Attack");
            attackManager.PlayerAttack(100, inputPlayer.lookDir/*GetCharacterAttributes().Damage*/); // maybe we can use events animation
        }
#endif

        return mov;
    }

    private Vector3 Flow(float axisHorizontal, float axisVertical)
    {
        Vector3 mov = new Vector3(axisHorizontal, axisVertical, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + mov,
            characterController.GetCharacterAttributes().Speed * Time.deltaTime);

        Horizontal = axisHorizontal;
        Vertical = axisVertical;

        return mov;
    }

    private void SetNPCAnimation()
    {
        // TODO

        if (Horizontal != 0 || Vertical != 0)
        {
            SetAnimPosition();
            //animator.SetBool(hasCodeRun, true);
        }
        else
        {
            //animator.SetBool(hasCodeRun, false);
        }

        /*if (Input.GetButtonDown("PlayerAttack"))
        {
            animator.SetBool("AttackEnable", true);
            //attackController.PlayerAttack(inputPlayer.lookDir, playerAttributes.Damage); // maybe we can use events animation
        }*/
    }

    private void SetAnimPosition()
    {
        animator.SetFloat("Horizontal", Horizontal);
        animator.SetFloat("Vertical", Vertical);
    }

    public InputPlayer GetInputPlayer()
    {
        if (inputPlayer == null)
            inputPlayer = GetComponent<InputPlayer>();

        return inputPlayer;
    }

    private void FlipSprite()
    {
        if (PhotonNetwork.InRoom)
        {
            // TODO ONLINE
        }
        else
        {
            if (Horizontal > 0 && Mathf.Abs(Vertical) < Mathf.Abs(Horizontal))
            {
                playerSprite.flipX = false;
            }
            else if (Horizontal != 0)
            {
                playerSprite.flipX = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
#if PLATFORM_IOS || UNITY_ANDROID

        MobileAction();
#endif
    }

    private void MobileAction()
    {
        RepositoryManager.Get<GameEvents>().RegisterEvent("PrimaryAction", () =>
        {
            animator.SetTrigger("Attack");
            attackManager.PlayerAttack(100, inputPlayer.lookDir/*GetCharacterAttributes().Damage*/); // maybe we can use events animation
        });
    }
}
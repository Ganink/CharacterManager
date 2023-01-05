using System;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(UserSettings))]
[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCManager : NPCManagerBase
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] bool canMove;

#if PLATFORM_IOS || UNITY_ANDROID
    [SerializeField]
    private JoystickManager joystick;
#endif

    private InputPlayer inputPlayer;
    private NPCController characterController;

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
            characterController = new NPCController();
            mainCamera = GetCamera();
            characterController.SetMainCamera(this.transform);
            MinimapSettings.Instance.SetMinimap(this.transform); //need abstract more this call
            playerSprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            canMove = true;
            //hasCodeRun = Animator.StringToHash("walk");
            
#if PLATFORM_IOS || UNITY_ANDROID
            joystick = FindObjectOfType<JoystickManager>(true);
            joystick.gameObject.SetActive(true);
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

        MoveNPC(NPCType.Player, inputPlayer);
    }

    public override void MovePlayer()
    {
        if (GetNPCType() == NPCType.Player)
        {
            if (canMove)
            {
                base.MovePlayer();
                Vector3 mov = new Vector3();

#if PLATFORM_IOS || UNITY_ANDROID
            mov = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
            transform.position = Vector3.MoveTowards(
                transform.position, transform.position + mov, GetCharacterAttributes().GetSpeedUser() * Time.deltaTime);

            Horizontal = joystick.Horizontal;
            Vertical = joystick.Vertical;
            
#else
                mov = new Vector3(inputPlayer.axisHorizontal, inputPlayer.axisVertical, 0);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + mov,
                    GetCharacterAttributes().GetSpeedUser() * Time.deltaTime);

                Horizontal = inputPlayer.axisHorizontal;
                Vertical = inputPlayer.axisVertical;
#endif                
            }

            SetNPCAnimation();
        }

        FlipSprite();
    }

    private void SetNPCAnimation()
    {
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
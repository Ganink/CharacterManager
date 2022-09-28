using UnityEngine;

[RequireComponent(typeof(InputPlayer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class NPCManager : NPCManagerBase
{    
    [SerializeField]
    private GameObject mainCamera;

    private Vector3 cameraPos;
    private InputPlayer inputPlayer;
    private NPCController characterController;

    private float Horizontal;
    private float Vertical;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        SetNPC();
        inputPlayer = GetComponent<InputPlayer>();
        characterController = new NPCController();
        mainCamera = characterController.GetMainCamera();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        base.Update();
        //JumpController(NPCType.Player, inputPlayer);
        MoveNPC(NPCType.Player, inputPlayer);

        if (inputPlayer.isInventoryEnable)
        {
            var inventory = GameObject.FindObjectOfType<InventoryManager>(true);
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
        }
    }

    public override void MovePlayer()
    {
        base.MovePlayer();
        Vector3 mov = new Vector3();
        mov = new Vector3(inputPlayer.axisHorizontal, inputPlayer.axisVertical, 0);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + mov, GetCharacterAttributes().GetSpeedUser() * Time.deltaTime);

        Horizontal = inputPlayer.axisHorizontal;
        Vertical = inputPlayer.axisVertical;

        FlipSprite();
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
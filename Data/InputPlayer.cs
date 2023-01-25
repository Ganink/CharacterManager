using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    //controls
    public float axisHorizontal { get; private set; }
    public float axisVertical { get; private set; }
    [HideInInspector] public Vector2 lookDir = new Vector2(0, -1);

    //actions
    public bool isAttack { get; private set; }
    public bool isJump { get; private set; }
    public bool isSpecialAttack1Enable { get; private set; }
    public bool isSpecialAttack2Enable { get; private set; }

    //inventory
    public bool isInventoryEnable { get; private set; }

    //Action
    public bool isActionEnable { get; private set; }

    public bool LogActive = false;

    void Update()
    {
        InputControls();
    }

    private void InputControls()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");

        //isJump = Input.GetButton("Jump");
        isInventoryEnable = Input.GetButton("Inventory");
        isActionEnable = Input.GetButton("Action");
        isAttack = Input.GetButton("Attack");

        SetLookDir();

        if (LogActive == true)
        {
            Debug.Log("isAttack?: " + isAttack);
            Debug.Log("specialAttack1 ?: " + isSpecialAttack1Enable);
            Debug.Log("specialAttack2?: " + isSpecialAttack2Enable);
            Debug.Log("isInventoryEnable?: " + isInventoryEnable);
            Debug.Log("playerActionEnable?: " + isActionEnable);
        }
    }

    public void SetPrimaryAction(bool value)
    {
        isActionEnable = value;
    }

    public bool GetPrimaryActionPressed()
    {
        return isActionEnable;
    }

    private void SetLookDir()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3)lookDir.normalized * 3, Color.green);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            lookDir = new Vector2(axisHorizontal, axisVertical);
        }
    }

}

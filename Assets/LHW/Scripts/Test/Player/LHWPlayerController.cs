using UnityEngine;

/// <summary>
/// (For Test) Player Controller and Move.
/// </summary>
public class LHWPlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject craftingPanel;

    private Rigidbody2D _rigid;
    private Vector2 inputVec;

    private void Awake() => Init();

    private void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (craftingPanel.activeSelf == false)
            {
                craftingPanel.SetActive(true);
            }
            else
            {
                craftingPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(inventoryPanel.activeSelf == false)
            {
                inventoryPanel.SetActive(true);
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputVec = new Vector2(x, y).normalized;
    }

    private void Move()
    {
        _rigid.velocity = inputVec * playerSpeed;
    }
}
using UnityEngine;

/// <summary>
/// (For Test) Player Controller and Move.
/// </summary>
public class LHWPlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _craftingPanel;
    [SerializeField] private GameObject _decompositionPanel;

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
            if (_craftingPanel.activeSelf == false)
            {
                _craftingPanel.SetActive(true);
            }
            else
            {
                _craftingPanel.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(_inventoryPanel.activeSelf == false)
            {
                _inventoryPanel.SetActive(true);
            }
            else
            {
                _inventoryPanel.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (_decompositionPanel.activeSelf == false)
            {
                _decompositionPanel.SetActive(true);
            }
            else
            {
                _decompositionPanel.SetActive(false);
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
        _rigid.velocity = inputVec * _playerSpeed;
    }
}
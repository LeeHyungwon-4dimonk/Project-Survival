using UnityEngine;

public class TestBoxInteractor : MonoBehaviour
{
    [SerializeField] GameObject _boxUI;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _boxUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _boxUI.SetActive(false);
        }
    }
}

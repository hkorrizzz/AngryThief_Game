using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShootArea : MonoBehaviour
{
    [SerializeField] private LayerMask _slingShotAreaMask;
    public bool IsWithingSlingshotArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Physics2D.OverlapPoint(worldPosition, _slingShotAreaMask))
        {
            return true;
        }
        else { return false; }

    }
}

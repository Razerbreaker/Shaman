using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField]
    private Collider2D other = null;
    [SerializeField]
    private Collider2D other1 = null;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other1, true);
    }
}
